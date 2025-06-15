import React, { useState, useEffect } from 'react';
import axios from 'axios';

const FormAluno = ({ alunoEditando, onSave }) => {
  const [nome, setNome] = useState('');
  const [matricula, setMatricula] = useState('');

  useEffect(() => {
    if (alunoEditando) {
      setNome(alunoEditando.nome);
      setMatricula(alunoEditando.matricula);
    }
  }, [alunoEditando]);

  const handleSubmit = async (e) => {
    e.preventDefault();

    const novoAluno = { nome, matricula: parseInt(matricula) };

    if (alunoEditando) {
  await axios.put(
    `${process.env.REACT_APP_API_URL}/api/aluno/${alunoEditando.id}`,
    {
      ...novoAluno,
      id: alunoEditando.id,
    }
  );
} else {
  await axios.post(
    `${process.env.REACT_APP_API_URL}/api/aluno`,
    novoAluno
  );
}


    setNome('');
    setMatricula('');
    onSave();
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>{alunoEditando ? "Editar Aluno" : "Novo Aluno"}</h2>
      <input
        type="text"
        placeholder="Nome"
        value={nome}
        onChange={e => setNome(e.target.value)}
        required
      />
      <input
        type="number"
        placeholder="Matrícula"
        value={matricula}
        onChange={e => setMatricula(e.target.value)}
        required
      />
      <button type="submit">{alunoEditando ? "Atualizar" : "Cadastrar"}</button>
    </form>
  );
};

export default FormAluno;
