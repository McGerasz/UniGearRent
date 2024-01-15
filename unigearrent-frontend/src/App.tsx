import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/js/bootstrap.bundle';
import logo from './logo.svg';
import './App.css';
import {BrowserRouter, createBrowserRouter, RouterProvider, Route} from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'
import Layout from './Pages/Layout';
import LoginPage from './Pages/LoginPage';
import HomePage from './Pages/HomePage';
import RegisterPage from './Pages/RegisterPage';
import { UserProfileProvider, useUserProfile } from './Utils/UserProfileContextProvider';
import Cookies from 'universal-cookie';
import SearchPage from './Pages/SearchPage';

function App() {
  let cookies = new Cookies();
  let setter = useUserProfile().setUserProfile;
  useEffect(() => {
    let profile = cookies.get("profile");
    if(profile) setter(profile);
  }, [])
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
        },{
          path: "/search",
          element: <SearchPage />
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
