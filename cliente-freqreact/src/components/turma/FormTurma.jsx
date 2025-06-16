import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Plus, Save } from 'lucide-react';
import './formTurma.css';

const FormTurma = ({ onSave, turmaEditando }) => {
  const [nome, setNome] = useState('');
  const [ano, setAno] = useState('');
  const [professorId, setProfessorId] = useState('');
  const [professores, setProfessores] = useState([]);

  useEffect(() => {
    axios.get(`${process.env.REACT_APP_API_URL}/api/professorapi`)
      .then(res => setProfessores(res.data))
      .catch(err => console.error("Erro ao carregar professores:", err));
  }, []);

  useEffect(() => {
    if (turmaEditando) {
      setNome(turmaEditando.nome || '');
      setAno(turmaEditando.ano || '');
      setProfessorId(turmaEditando.professorId || '');
    }
  }, [turmaEditando]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const novaTurma = { nome, ano, professorId };
    console.log("Turma a ser enviada:", novaTurma);

    try {
      if (turmaEditando) {
        await axios.put(`${process.env.REACT_APP_API_URL}/api/turmaapi/${turmaEditando.id}`, {
          id: turmaEditando.id,
          ...novaTurma,
        });
      } else {
        await axios.post(`${process.env.REACT_APP_API_URL}/api/turmaapi`, novaTurma);
      }

      setNome('');
      setAno('');
      setProfessorId('');
      onSave();
    } catch (err) {
      console.error("Erro ao salvar turma:", err);
    }
  };

  return (
    <div className="form-turma-card">
      <h2>{turmaEditando ? "Editar Turma" : "Nova Turma"} <Plus size={20} /></h2>
      <form onSubmit={handleSubmit} className="form-turma">
        <label>Nome:</label>
        <input
          type="text"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
        />

        <label>Ano:</label>
        <input
          type="text"
          value={ano}
          onChange={(e) => setAno(e.target.value)}
          required
        />

        <label>Professor:</label>
        <select
          value={professorId}
          onChange={(e) => setProfessorId(e.target.value)}
          required
        >
          <option value="">Selecione</option>
          {professores.map(p => (
            <option key={p.id} value={p.id}>{p.nome}</option>
          ))}
        </select>

        <button type="submit" className="btn-salvar">
          <Save size={16} style={{ marginRight: "6px" }} />
          Salvar
        </button>
      </form>
    </div>
  );
};

export default FormTurma;
