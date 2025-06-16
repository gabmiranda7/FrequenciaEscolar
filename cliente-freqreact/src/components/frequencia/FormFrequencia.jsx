import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './FormFrequencia.css';


const FormFrequencia = ({ frequenciaEditando, onSave }) => {
  const [alunos, setAlunos] = useState([]);
  const [turmas, setTurmas] = useState([]);
  const [alunoId, setAlunoId] = useState('');
  const [turmaId, setTurmaId] = useState('');
  const [data, setData] = useState('');
  const [presente, setPresente] = useState('true');

  useEffect(() => {
    axios.get(`${process.env.REACT_APP_API_URL}/api/aluno`)
      .then(res => setAlunos(res.data))
      .catch(err => console.error("Erro ao buscar alunos", err));

    axios.get(`${process.env.REACT_APP_API_URL}/api/turmaapi`)
      .then(res => setTurmas(res.data))
      .catch(err => console.error("Erro ao buscar turmas", err));
  }, []);

  useEffect(() => {
    if (frequenciaEditando) {
      setAlunoId(frequenciaEditando.alunoId || '');
      setTurmaId(frequenciaEditando.turmaId || '');
      setData(frequenciaEditando.data?.substring(0, 10) || '');
      setPresente(frequenciaEditando.presente ? 'true' : 'false');
    }
  }, [frequenciaEditando]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const novaFrequencia = {
      alunoId: parseInt(alunoId),
      turmaId: parseInt(turmaId),
      data,
      presente: presente === 'true'
    };

    try {
      if (frequenciaEditando) {
        await axios.put(
          `${process.env.REACT_APP_API_URL}/api/frequenciaapi/${frequenciaEditando.id}`,
          { ...novaFrequencia, id: frequenciaEditando.id }
        );
      } else {
        await axios.post(`${process.env.REACT_APP_API_URL}/api/frequenciaapi`, novaFrequencia);
      }

      // limpar
      setAlunoId('');
      setTurmaId('');
      setData('');
      setPresente('true');
      onSave();
    } catch (error) {
      console.error("Erro ao salvar frequência:", error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>{frequenciaEditando ? "Editar Frequência" : "Registrar Frequência"}</h2>

      <select value={alunoId} onChange={(e) => setAlunoId(e.target.value)} required>
        <option value="">Selecione um aluno</option>
        {alunos.map((a) => (
          <option key={a.id} value={a.id}>{a.nome}</option>
        ))}
      </select>

      <select value={turmaId} onChange={(e) => setTurmaId(e.target.value)} required>
        <option value="">Selecione uma turma</option>
        {turmas.map((t) => (
          <option key={t.id} value={t.id}>{t.nome}</option>
        ))}
      </select>

      <input
        type="date"
        value={data}
        onChange={(e) => setData(e.target.value)}
        required
      />

      <select value={presente} onChange={(e) => setPresente(e.target.value)} required>
        <option value="true">Presente</option>
        <option value="false">Faltou</option>
      </select>

      <button type="submit">
        {frequenciaEditando ? "Atualizar" : "Salvar"}
      </button>
    </form>
  );
};

export default FormFrequencia;
