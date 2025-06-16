import React, { useEffect, useState } from 'react';
import axios from 'axios';
import './listaProfessor.css';
import { Pencil, Trash2 } from 'lucide-react';

const ListaProfessor = ({ onEdit, onDelete }) => {
    const [professores, setProfessores] = useState([]);

    const carregarProfessores = async () => {
        try {
            const res = await axios.get(`${process.env.REACT_APP_API_URL}/api/professorapi`);
            setProfessores(Array.isArray(res.data) ? res.data : []);
        } catch (err) {
            console.error("Erro ao buscar professores:", err);
            setProfessores([]);
        }
    };

    useEffect(() => {
        carregarProfessores();
    }, []);

    return (
        <div className="lista-prof-container">
            <h2>Professores Cadastrados</h2>
            <div className="grid-prof">
                {Array.isArray(professores) && professores.map((p) => (
                    <div key={p.id} className="card-prof">
                        <h3>{p.nome}</h3>
                        <p><strong>Disciplina:</strong> {p.disciplina}</p>
                        <div className="card-prof-botoes">
                            <button onClick={() => onEdit(p)} className="editar">
                                <Pencil size={16} /> Editar
                            </button>
                            <button onClick={() => onDelete(p.id)} className="remover">
                                <Trash2 size={16} /> Remover
                            </button>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default ListaProfessor;
