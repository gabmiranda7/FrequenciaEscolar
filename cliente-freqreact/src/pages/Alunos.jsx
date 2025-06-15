import React, { useState } from 'react';
import ListaAlunos from '../components/aluno/ListaAlunos';
import FormAluno from '../components/aluno/FormAluno';
import { UserPlus, Users } from 'lucide-react';
import './Alunos.css';

const Alunos = () => {
  const [alunoEditando, setAlunoEditando] = useState(null);
  const [contador, setContador] = useState(0);

  const recarregar = () => {
    setContador(prev => prev + 1);
    setAlunoEditando(null);
  };

  return (
    <div className="alunos-container">
      <div className="alunos-header-card">
        <h1 className="titulo-pagina">
          <Users size={28} /> Gestão de Alunos
        </h1>
      </div>

      <div className="alunos-card">
        <h2 className="card-titulo"><UserPlus size={20} /> {alunoEditando ? "Editar Aluno" : "Novo Aluno"}</h2>
        <FormAluno alunoEditando={alunoEditando} onSave={recarregar} />
      </div>

      <div className="alunos-card">
        <h2 className="card-titulo"><Users size={20} /> Lista de Alunos</h2>
        <ListaAlunos key={contador} onEdit={setAlunoEditando} />
      </div>
    </div>
  );
};

export default Alunos;
