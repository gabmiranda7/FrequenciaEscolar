import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ListaFrequencia.css';

const ListaFrequencia = () => {
  const [frequencias, setFrequencias] = useState([]);

  useEffect(() => {
    axios.get(`${process.env.REACT_APP_API_URL}/api/frequenciaapi`)
      .then(res => setFrequencias(res.data))
      .catch(err => console.error("Erro ao carregar frequências:", err));
  }, []);

  return (
    <div className="lista-frequencia-container">
      <h2 className="titulo-frequencia">Frequências Registradas</h2>

      {frequencias.length === 0 ? (
        <p className="mensagem-vazia">Nenhuma frequência registrada ainda.</p>
      ) : (
        <div className="frequencia-grid">
          {frequencias.map((f) => (
            <div key={f.id} className="frequencia-card">
              <p><strong>Aluno:</strong> {f.aluno?.nome || `#${f.alunoId}`}</p>
              <p><strong>Turma:</strong> {f.turma?.nome || `#${f.turmaId}`}</p>
              <p><strong>Data:</strong> {new Date(f.data).toLocaleDateString()}</p>
              <p><strong>Status:</strong> <span className={f.presente ? "presente" : "faltou"}>
                {f.presente ? 'Presente' : 'Faltou'}
              </span></p>
            </div>
          ))}
        </div>
      )}
    </div>
  );
};

export default ListaFrequencia;
