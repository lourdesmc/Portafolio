import React from 'react';

const Inicio = ({children}) =>
    <div className="App">
        <header id="main-header">
            <div id="logo">
                <h2 id="titPral">Goodreads</h2>
            </div>
            <nav id="main-nav">
                <a href="" className="menu">Inicio</a>
                <a href="" className="menu">Cat&aacute;logo</a>
                <a href="" className="menu">Acerca de</a>
            </nav>
        </header>
        <main id="main-content">
            {children}
        </main>

    </div>

export {Inicio}