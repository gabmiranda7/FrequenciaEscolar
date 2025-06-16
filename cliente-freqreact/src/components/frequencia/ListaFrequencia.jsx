import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './ListaFrequencia.css'; // Se quiser customizar via CSS

const ListaFrequencia = () => {
  const [frequencias, setFrequencias] = useState([]);

  useEffect(() => {
    axios.get(`${process.env.REACT_APP_API_URL}/api/frequenciaapi`)
      .then(res => setFrequencias(res.data))
      .catch(err => console.error("Erro ao carregar frequências:", err));
  }, []);

  return (
    <div className="lista-frequencia-container">
      <h2>Frequências Registradas</h2>

      {frequencias.length === 0 ? (
        <p>Nenhuma frequência registrada ainda.</p>
      ) : (
        <ul className="lista-frequencia">
          {frequencias.map((f) => (
            <li key={f.id} className="frequencia-item">
              <strong>Aluno:</strong> {f.aluno?.nome || `#${f.alunoId}`}<br />
              <strong>Turma:</strong> {f.turma?.nome || `#${f.turmaId}`}<br />
              <strong>Data:</strong> {new Date(f.data).toLocaleDateString()}<br />
              <strong>Status:</strong> {f.presente ? 'Presente' : 'Faltou'}
            </li>
          ))}
        </ul>
      )}
    </div>
  );
};

export default ListaFrequencia;
