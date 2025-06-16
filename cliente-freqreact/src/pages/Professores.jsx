import React, { useState } from 'react';
import FormProfessor from '../components/professor/FormProfessor';
import ListaProfessor from '../components/professor/ListaProfessor';
import axios from 'axios';
import './professores.css';

const Professores = () => {
  const [reload, setReload] = useState(0);
  const [editando, setEditando] = useState(null);

  const handleSalvar = () => {
    setEditando(null);
    setReload(prev => prev + 1);
  };

  const handleEditar = (professor) => {
    setEditando(professor);
  };

  const handleDeletar = async (id) => {
    try {
      await axios.delete(`${process.env.REACT_APP_API_URL}/api/professorapi/${id}`);
      setReload(prev => prev + 1);
    } catch (error) {
      console.error("Erro ao deletar professor:", error);
    }
  };

  return (
    <div className="prof-container">
      <h1>Gestão de Professores</h1>
      <FormProfessor onSave={handleSalvar} professorEditando={editando} />
      <ListaProfessor key={reload} onEdit={handleEditar} onDelete={handleDeletar} />
    </div>
  );
};

export default Professores;
