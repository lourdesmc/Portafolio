import './App.css';
import {Inicio} from "./components/index";
import {Formulario} from "./components/formulario";
import 'bootstrap/dist/css/bootstrap.min.css';

function App() {
  return (
    <div>
      <Inicio>
        <Formulario />
      </Inicio>
    </div>
  );
}

export default App;
