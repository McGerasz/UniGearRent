import React from 'react';
import logo from './logo.svg';
import './App.css';
import {BrowserRouter, createBrowserRouter, RouterProvider, Route} from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'
import Layout from './Pages/Layout';

function App() {
  const router = createBrowserRouter([
    {
      path: "/",
      element: <Layout/>,
      errorElement: <Layout/>
    }
  ]);
  return (
    <>
    <RouterProvider router={router} />
    </>
  );
}

export default App;
