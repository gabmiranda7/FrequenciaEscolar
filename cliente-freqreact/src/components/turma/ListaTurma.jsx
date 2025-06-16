import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ListaTurma.css';

const ListaTurma = ({ onEdit, refreshKey }) => {
  const [turmas, setTurmas] = useState([]);
  const carregarTurmas = async () => {
    try {
      const res = await axios.get(`${process.env.REACT_APP_API_URL}/api/turmaapi`);
      console.log("Turmas recebidas:", res.data); // debug
      setTurmas(res.data);
    } catch (err) {
      console.error('Erro ao carregar turmas:', err);
      setTurmas([]); // fallback para garantir que não será undefined
    }
  };

  const deletarTurma = async (id) => {
    if (window.confirm("Tem certeza que deseja remover esta turma?")) {
      await axios.delete(`${process.env.REACT_APP_API_URL}/api/turmaapi/${id}`);
      carregarTurmas();
    }
  };

  useEffect(() => {
    carregarTurmas();
  }, [refreshKey]);
  return (
    <div className="lista-turmas-container">
      <h2>Lista de Turmas</h2>
      <div className="turma-cards">
        {Array.isArray(turmas) ? (
          turmas.map((turma) => (
            <div className="turma-card" key={turma.id}>
              <h3>{turma.nome}</h3>
              <p><strong>Ano:</strong> {turma.ano}</p>
              <p><strong>Professor:</strong> {turma.professor?.nome || turma.professorId}</p>
              <div className="turma-card-buttons">
                <button onClick={() => onEdit(turma)} className="btn editar">Editar</button>
                <button onClick={() => deletarTurma(turma.id)} className="btn deletar">Excluir</button>
              </div>
            </div>
          ))
        ) : (
          <p>Nenhuma turma encontrada ou erro na resposta da API.</p>
        )}
      </div>
    </div>
  );
};

export default ListaTurma;
