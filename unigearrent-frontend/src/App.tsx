import React from 'react';
import logo from './logo.svg';
import './App.css';
import {BrowserRouter, createBrowserRouter, RouterProvider, Route} from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'
import Layout from './Pages/Layout';
import LoginPage from './Pages/LoginPage';
import HomePage from './Pages/HomePage';
import RegisterPage from './Pages/RegisterPage';

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
      errorElement: <Layout/>,
      children:[
        {
          path: "/",
          element: <HomePage />
        },
        {
          path: "/login",
          element: <LoginPage />
        },{
          path: "/register",
          element: <RegisterPage />
        }
      ]
    }
  ]);
  return (
    <>
    <RouterProvider router={router} />
    </>
  );
}

export default App;
