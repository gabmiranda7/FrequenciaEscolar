import React, { useState } from 'react';
import FormFrequencia from '../components/frequencia/FormFrequencia.jsx';
import ListaFrequencia from '../components/frequencia/ListaFrequencia';

const Frequencias = () => {
  const [reload, setReload] = useState(0);
  return (
    <div>
      <FormFrequencia onSave={() => setReload(prev => prev + 1)} />
      <ListaFrequencia key={reload} />
    </div>
  );
};

export default Frequencias;
