import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Home from "./pages/Home";
import Alunos from "./pages/Alunos";
import Professores from "./pages/Professores";
import Turmas from "./pages/Turmas";
import Frequencias from "./pages/Frequencias";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/alunos" element={<Alunos />} />
        <Route path="/professores" element={<Professores />} />
        <Route path="/turmas" element={<Turmas />} />
        <Route path="/frequencias" element={<Frequencias />} />
      </Routes>
    </Router>
  );
}

export default App;
