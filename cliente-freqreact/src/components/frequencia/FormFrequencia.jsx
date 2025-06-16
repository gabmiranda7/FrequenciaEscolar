import React, { useEffect, useState } from 'react';
import axios from 'axios';

const FormFrequencia = ({ onSave }) => {
  const [alunos, setAlunos] = useState([]);
  const [alunoId, setAlunoId] = useState('');
  const [turmaId, setTurmaId] = useState('');
  const [data, setData] = useState('');
  const [presente, setPresente] = useState('true'); // como string para select

  useEffect(() => {
    axios.get(`${process.env.REACT_APP_API_URL}/api/aluno`)
      .then(res => setAlunos(res.data))
      .catch(err => console.error("Erro ao buscar alunos", err));
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const novaFrequencia = {
      alunoId: parseInt(alunoId),
      turmaId: parseInt(turmaId),
      data,
      presente: presente === 'true'
    };

    try {
      await axios.post(`${process.env.REACT_APP_API_URL}/api/frequencia`, novaFrequencia);
      onSave();

      // limpar campos
      setAlunoId('');
      setTurmaId('');
      setData('');
      setPresente('true');
    } catch (error) {
      console.error("Erro ao salvar frequência:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Registrar Frequência</h2>

      <label>Aluno</label>
      <select value={alunoId} onChange={(e) => setAlunoId(e.target.value)} required>
        <option value="">Selecione um aluno</option>
        {alunos.map((a) => (
          <option key={a.id} value={a.id}>{a.nome}</option>
        ))}
      </select>

      <label>ID da Turma</label>
      <input
        type="number"
        value={turmaId}
        onChange={(e) => setTurmaId(e.target.value)}
        required
      />

      <label>Data</label>
      <input
        type="date"
        value={data}
        onChange={(e) => setData(e.target.value)}
        required
      />

      <label>Presente?</label>
      <select value={presente} onChange={(e) => setPresente(e.target.value)} required>
        <option value="true">Sim</option>
        <option value="false">Não</option>
      </select>

      <button type="submit">Salvar Frequência</button>
    </form>
  );
};

export default FormFrequencia;
