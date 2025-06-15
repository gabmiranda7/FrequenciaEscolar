import React from "react";
import { useNavigate } from "react-router-dom";

const Home = () => {
  const navigate = useNavigate();

  return (
    <div style={{
      display: "flex",
      justifyContent: "center",
      alignItems: "center",
      height: "100vh",
      background: "#f0f4f8",
    }}>
      <div style={{
        background: "white",
        padding: "3rem",
        borderRadius: "16px",
        boxShadow: "0 4px 12px rgba(0,0,0,0.1)",
        textAlign: "center",
        width: "90%",
        maxWidth: "500px"
      }}>
        <h1 style={{ marginBottom: "2rem", color: "#333" }}>
          Sistema de Frequência Escolar
        </h1>
        <div style={{
          display: "flex",
          flexDirection: "column",
          gap: "1rem"
        }}>
          <button style={buttonStyle} onClick={() => navigate("/alunos")}>Alunos</button>
          <button style={buttonStyle} onClick={() => navigate("/professores")}>Professores</button>
          <button style={buttonStyle} onClick={() => navigate("/turmas")}>Turmas</button>
          <button style={buttonStyle} onClick={() => navigate("/frequencias")}>Frequências</button>
        </div>
      </div>
    </div>
  );
};

const buttonStyle = {
  padding: "0.8rem 1rem",
  fontSize: "1rem",
  border: "none",
  borderRadius: "8px",
  background: "#1976d2",
  color: "white",
  cursor: "pointer",
  transition: "background 0.3s",
};

export default Home;
