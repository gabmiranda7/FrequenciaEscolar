import React, { useState, useEffect } from 'react';
import axios from 'axios';

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
    <form onSubmit={handleSubmit} className="form-container">
      <h2>{turmaEditando ? "Editar Turma" : "Cadastrar Turma"}</h2>

      <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={(e) => setNome(e.target.value)}
        required
      />

      <input
        type="text"
        placeholder="Ano"
        value={ano}
        onChange={(e) => setAno(e.target.value)}
        required
      />

      <select
        value={professorId}
        onChange={(e) => setProfessorId(e.target.value)}
        required
      >
        <option value="">Selecione um professor</option>
        {professores.map(p => (
          <option key={p.id} value={p.id}>{p.nome}</option>
        ))}
      </select>

      <button type="submit">
        {turmaEditando ? "Atualizar" : "Salvar"}
      </button>
    </form>
  );
};

export default FormTurma;
