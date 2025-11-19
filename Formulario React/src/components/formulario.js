import React, { useState } from 'react';

const Formulario = () => {
    const [nombre, setNombre] = useState('');
    const [apellidoPaterno, setApellidoPaterno] = useState('');
    const [apellidoMaterno, setApellidoMaterno] = useState('');
    const [fechaNacimiento, setFechaNacimiento] = useState('');
    const [colorEmpastado, setColorEmpastado] = useState('');
    const [portadaLibro, setPortadaLibro] = useState(null);
    const [urlTiendaLibros, setUrlTiendaLibros] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [direccion, setDireccion] = useState('');
    const [tipoLectura, setTipoLectura] = useState('');
    const [periodosLiterarios, setPeriodosLiterarios] = useState({
        clasico: false,
        barroco: false,
        medieval: false,
        romanticismo: false,
        neoclasico: false
    });
    const [registros, setRegistros] = useState([]);

    const cambiar = (e) => {
        const { name, value, type, checked, files } = e.target;
        if (type === 'checkbox') {
            setPeriodosLiterarios({
                ...periodosLiterarios,
                [name]: checked
            });
        } else if (type === 'file') {
            setPortadaLibro(files[0]);
        } else {
            switch (name) {
                case 'nombre':
                    setNombre(value);
                    break;
                case 'apellidoPaterno':
                    setApellidoPaterno(value);
                    break;
                case 'apellidoMaterno':
                    setApellidoMaterno(value);
                    break;
                case 'fechaNacimiento':
                    setFechaNacimiento(value);
                    break;
                case 'colorEmpastado':
                    setColorEmpastado(value);
                    break;
                case 'urlTiendaLibros':
                    setUrlTiendaLibros(value);
                    break;
                case 'email':
                    setEmail(value);
                    break;
                case 'password':
                    setPassword(value);
                    break;
                case 'direccion':
                    setDireccion(value);
                    break;
                case 'tipoLectura':
                    setTipoLectura(value);
                    break;
                default:
                    break;
            }
        }
    };

    const limpiarFormulario = () => {
        setNombre('');
        setApellidoPaterno('');
        setApellidoMaterno('');
        setFechaNacimiento('');
        setColorEmpastado('');
        setPortadaLibro(null);
        setUrlTiendaLibros('');
        setEmail('');
        setPassword('');
        setDireccion('');
        setTipoLectura('');
        setPeriodosLiterarios({
            clasico: false,
            barroco: false,
            medieval: false,
            romanticismo: false,
            neoclasico: false
        });
    }

    const onSubmit = (e) => {
        e.preventDefault();
        const datosForm = {
            nombre,
            apellidoPaterno,
            apellidoMaterno,
            fechaNacimiento,
            colorEmpastado,
            portadaLibro: portadaLibro ? URL.createObjectURL(portadaLibro) : null,
            urlTiendaLibros,
            email,
            password,
            direccion,
            tipoLectura,
            periodosLiterarios: Object.keys(periodosLiterarios).filter(key => periodosLiterarios[key])
        };
        setRegistros([...registros, datosForm]);
        console.log(datosForm);
        alert('Formulario enviado.');
        limpiarFormulario();
    };

    return (
        <section className="container mt-5">
            <header>
                <h3>Formulario de biblioteca</h3>
            </header>
            <form onSubmit={onSubmit}>
                <div className="row g-3 mb-3">
                    <div className="col-md-4 form-group">
                        <label htmlFor="nombre" className="form-label">Nombre:</label>
                        <input type="text" className="form-control" id="nombre" name="nombre" value={nombre} onChange={cambiar} required/>
                    </div>

                    <div className="col-md-4 form-group">
                        <label htmlFor="apellidoPaterno" className="form-label">Apellido paterno:</label>
                        <input type="text" className="form-control" id="apellidoPaterno" name="apellidoPaterno" value={apellidoPaterno} onChange={cambiar} required/>
                    </div>

                    <div className="col-md-4 form-group">
                        <label htmlFor="apellidoMaterno" className="form-label">Apellido materno:</label>
                        <input type="text" className="form-control" id="apellidoMaterno" name="apellidoMaterno" value={apellidoMaterno} onChange={cambiar} required/>
                    </div>
                </div>

                <div className="row g-3 mb-3">
                    <div className="col-md-4 form-group">
                        <label htmlFor="fechaNacimiento" className="form-label">Fecha de nacimiento:</label>
                        <input type="date" className="form-control" id="fechaNacimiento" name="fechaNacimiento" value={fechaNacimiento} onChange={cambiar} required/>
                    </div>
                    <div className="col-md-4 form-group">
                        <label htmlFor="colorEmpastado" className="form-label">Color de empastado:</label>
                        <input type="color" className="form-control" id="colorEmpastado" name="colorEmpastado" value={colorEmpastado} onChange={cambiar} required/>
                    </div>
                    <div className="col-md-4 form-group">
                        <label htmlFor="portadaLibro" className="form-label">Portada del libro:</label>
                        <input type="file" className="form-control" id="portadaLibro" name="portadaLibro" onChange={cambiar} required/>
                    </div>
                </div>

                <div className="row g-3 mb-3">
                    <div className="col-md-4 form-group">
                        <label htmlFor="urlTiendaLibros" className="form-label">URL de la tienda:</label>
                        <input type="url" className="form-control" id="urlTiendaLibros" name="urlTiendaLibros" value={urlTiendaLibros} onChange={cambiar} required/>
                    </div>
                    <div className="col-md-4 form-group">
                        <label htmlFor="email" className="form-label">Email:</label>
                        <input type="email" className="form-control" id="email" name="email" value={email} onChange={cambiar} required/>
                    </div>
                    <div className="col-md-4 form-group">
                        <label htmlFor="password" className="form-label">Contraseña:</label>
                        <input type="password" className="form-control" id="password" name="password" pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d{2})(?=.*[@-])(?=.*\d{3}).{6,}$"
                               value={password} onChange={cambiar} required/>

                    </div>
                </div>

                <div className="row g-3 mb-3">
                    <div className="col-md-4 form-group">
                        <label htmlFor="direccion" className="form-label">Direcci&oacute;n:</label>
                        <input type="text" className="form-control" id="direccion" name="direccion" value={direccion} onChange={cambiar} />
                    </div>
                    <div className="col-md-4 form-group">
                        <label className="form-label">Periodos literarios:</label>
                        <div className="form-check">
                            <input type="checkbox" className="form-check-input" id="clasico" name="clasico" checked={periodosLiterarios.clasico} onChange={cambiar} />
                            <label className="form-check-label" htmlFor="clasico">Cl&aacute;sico</label>
                        </div>
                        <div className="form-check">
                            <input type="checkbox" className="form-check-input" id="barroco" name="barroco" checked={periodosLiterarios.barroco} onChange={cambiar} />
                            <label className="form-check-label" htmlFor="barroco">Barroco</label>
                        </div>
                        <div className="form-check">
                            <input type="checkbox" className="form-check-input" id="medieval" name="medieval" checked={periodosLiterarios.medieval} onChange={cambiar} />
                            <label className="form-check-label" htmlFor="medieval">Medieval</label>
                        </div>
                        <div className="form-check">
                            <input type="checkbox" className="form-check-input" id="romanticismo" name="romanticismo" checked={periodosLiterarios.romanticismo} onChange={cambiar} />
                            <label className="form-check-label" htmlFor="romanticismo">Romanticismo</label>
                        </div>
                        <div className="form-check">
                            <input type="checkbox" className="form-check-input" id="neoclasico" name="neoclasico" checked={periodosLiterarios.neoclasico} onChange={cambiar} />
                            <label className="form-check-label" htmlFor="neoclasico">Neocl&aacute;sico</label>
                        </div>
                    </div>
                    <div className="col-md-4 form-group">
                        <label htmlFor="tipoLectura" className="form-label">Tipo de lectura:</label>
                        <select className="form-select" id="tipoLectura" name="tipoLectura" value={tipoLectura} onChange={cambiar} required>
                            <option value="">Selecciona una opción</option>
                            <option value="novela">Novela</option>
                            <option value="cuento">Cuento</option>
                            <option value="poesia">Poes&iacute;a</option>
                            <option value="ensayo">Ensayo</option>
                        </select>
                    </div>
                </div>

                <button type="submit" className="btn btn-outline-light btn-lg">Enviar</button>

                <br />
            </form>

            <section className="mt-5">
                <h3>Registros</h3>
                <table className="table table-bordered">
                    <thead>
                    <tr>
                        <th>Nombre</th>
                        <th>Apellido paterno</th>
                        <th>Apellido materno</th>
                        <th>Fecha de nacimiento</th>
                        <th>Color de empastado</th>
                        <th>Portada del libro</th>
                        <th>URL de la tienda</th>
                        <th>Email</th>
                        <th>Contraseña</th>
                        <th>Direcci&oacute;n</th>
                        <th>Tipo de lectura</th>
                        <th>Periodos Literarios</th>
                    </tr>
                    </thead>
                    <tbody>
                    {registros.map((registro, index) => (
                        <tr key={index}>
                            <td>{registro.nombre}</td>
                            <td>{registro.apellidoPaterno}</td>
                            <td>{registro.apellidoMaterno}</td>
                            <td>{registro.fechaNacimiento}</td>
                            <td>
                                <div style={{ backgroundColor: registro.colorEmpastado, width: '30px', height: '30px' }}></div>
                            </td>
                            <td>
                                {registro.portadaLibro ? <img src={registro.portadaLibro} alt="Portada" width="50" height="50" /> : 'No imagen'}
                            </td>
                            <td>{registro.urlTiendaLibros}</td>
                            <td>{registro.email}</td>
                            <td>{registro.password}</td>
                            <td>{registro.direccion}</td>
                            <td>{registro.tipoLectura}</td>
                            <td>{registro.periodosLiterarios.join(', ')}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            </section>
        </section>
    );
};

export {Formulario};
