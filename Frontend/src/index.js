import 'bootstrap/dist/css/bootstrap.css';
import 'bootstrap/dist/js/bootstrap.bundle';
import 'bootstrap/dist/js/bootstrap.js';
import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './App';
import AlertTemplate from 'react-alert-template-basic'
import { transitions, positions, Provider as AlertProvider } from 'react-alert';
import { BrowserRouter } from "react-router-dom"


const options = {
  // you can also just use 'bottom center'
  position: positions.BOTTOM_CENTER,
  timeout: 5000,
  offset: '30px',
  // you can also just use 'scale'
  transition: transitions.SCALE
}

ReactDOM.render(
  <React.StrictMode>
    <BrowserRouter>
      <AlertProvider template={AlertTemplate} {...options}>
        <App />
      </AlertProvider>
    </BrowserRouter>
  </React.StrictMode>,
  document.getElementById('root')
);

