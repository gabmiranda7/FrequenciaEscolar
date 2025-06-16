import React, { useState } from 'react';
import ListaTurma from '../components/turma/ListaTurma';
import FormTurma from '../components/turma/FormTurma';
import './Turmas.css';

const Turmas = () => {
  const [turmaEditando, setTurmaEditando] = useState(null);
  const [refreshKey, setRefreshKey] = useState(0);


  const handleEdit = (turma) => {
    console.log("Editando turma:", turma); //testes
    setTurmaEditando(turma);
  };

  const handleSave = () => {
    setTurmaEditando(null); // limpa o estado
    setRefreshKey(prev => prev + 1); // força recarregamento da lista

  };
  

  return (
    <div className="turmas-container">
      <h1 className="titulo-turmas">Gerenciamento de Turmas</h1>

      <div className="turmas-conteudo">
        <FormTurma onSave={handleSave} turmaEditando={turmaEditando} />
        <ListaTurma onEdit={handleEdit} refreshKey={refreshKey} />

      </div>
    </div>
  );
};

export default Turmas;
