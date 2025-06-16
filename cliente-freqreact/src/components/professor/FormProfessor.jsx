import React, { useState, useEffect } from 'react';
import axios from 'axios';
import './formProfessor.css';

const FormProfessor = ({ professorEditando, onSave }) => {
  const [professor, setProfessor] = useState({ nome: '', disciplina: '' });

  useEffect(() => {
    if (professorEditando) {
      setProfessor(professorEditando);
    }
  }, [professorEditando]);

  const handleChange = (e) => {
    const { name, value } = e.target;
    setProfessor((prev) => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      if (professor.id) {
        await axios.put(`${process.env.REACT_APP_API_URL}/api/professorapi/${professor.id}`, professor);
      } else {
        await axios.post(`${process.env.REACT_APP_API_URL}/api/professorapi`, professor);
      }
      setProfessor({ nome: '', disciplina: '' });
      onSave();
    } catch (err) {
      console.error('Erro ao salvar professor:', err);
    }
  };

  return (
    <div className="form-container-prof">
      <h2>{professor.id ? 'Editar Professor' : 'Cadastrar Professor'}</h2>
      <form onSubmit={handleSubmit}>
        <label>
          Nome:
          <input
            type="text"
            name="nome"
            value={professor.nome}
            onChange={handleChange}
            required
          />
        </label>
        <label>
          Disciplina:
          <input
            type="text"
            name="disciplina"
            value={professor.disciplina}
            onChange={handleChange}
            required
          />
        </label>
        <button type="submit">{professor.id ? 'Atualizar' : 'Salvar'}</button>
      </form>
    </div>
  );
};

export default FormProfessor;
