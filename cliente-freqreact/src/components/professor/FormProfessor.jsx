import React, { useState, useEffect } from 'react';
import axios from 'axios';

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
    <form onSubmit={handleSubmit} className="form-container">
      <h2>{professor.id ? 'Editar Professor' : 'Cadastrar Professor'}</h2>

      <input
        type="text"
        name="nome"
        placeholder="Nome"
        value={professor.nome}
        onChange={handleChange}
        required
      />

      <input
        type="text"
        name="disciplina"
        placeholder="Disciplina"
        value={professor.disciplina}
        onChange={handleChange}
        required
      />

      <button type="submit">{professor.id ? 'Atualizar' : 'Salvar'}</button>
    </form>
  );
};

export default FormProfessor;
