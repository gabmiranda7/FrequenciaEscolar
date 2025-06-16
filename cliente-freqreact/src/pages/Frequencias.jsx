import React, { useState } from 'react';
import FormFrequencia from '../components/frequencia/FormFrequencia.jsx';
import ListaFrequencia from '../components/frequencia/ListaFrequencia';
import './Frequencias.css';

const Frequencias = () => {
  const [reload, setReload] = useState(0);

  return (
    <div className="frequencias-container">
      <h1 className="titulo-pagina">Gerenciamento de Frequência</h1>

      <div className="frequencias-conteudo">
        <FormFrequencia onSave={() => setReload(prev => prev + 1)} />
        <ListaFrequencia key={reload} />
      </div>
    </div>
  );
};

export default Frequencias;
