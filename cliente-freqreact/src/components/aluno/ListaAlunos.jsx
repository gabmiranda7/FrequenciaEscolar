import React, { useEffect, useState } from 'react';
import axios from 'axios';

const ListaAlunos = ({ onEdit }) => {
  const [alunos, setAlunos] = useState([]);

const API_URL = process.env.REACT_APP_API_URL;

const carregarAlunos = async () => {
  const res = await axios.get(`${API_URL}/api/aluno`);
  setAlunos(res.data);
};

const deletarAluno = async (id) => {
  await axios.delete(`${API_URL}/api/aluno/${id}`);
  carregarAlunos();
};


  useEffect(() => {
    carregarAlunos();
  }, []);

  return (
    <div>
      <h2>Lista de Alunos</h2>
      <table border="1" cellPadding={8}>
        <thead>
          <tr>
            <th>ID</th>
            <th>Nome</th>
            <th>Matrícula</th>
            <th>Ações</th>
          </tr>
        </thead>
        <tbody>
          {alunos.map(aluno => (
            <tr key={aluno.id}>
              <td>{aluno.id}</td>
              <td>{aluno.nome}</td>
              <td>{aluno.matricula}</td>
              <td>
                <button onClick={() => onEdit(aluno)}>Editar</button>
                <button onClick={() => deletarAluno(aluno.id)}>Excluir</button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ListaAlunos;
