import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/js/bootstrap.bundle';
import './App.css';
import {createBrowserRouter, RouterProvider} from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css'
import Layout from './Pages/Layout';
import LoginPage from './Pages/LoginPage';
import HomePage from './Pages/HomePage';
import RegisterPage from './Pages/RegisterPage';
import { useUserProfile } from './Utils/UserProfileContextProvider';
import Cookies from 'universal-cookie';
import SearchPage from './Pages/SearchPage';
import MyPostsPage from './Pages/MyPostsPage';
import CreatePostPage from './Pages/CreatePostPage';
import MyPostPage from './Pages/MyPostPage';
import EditPostPage from './Pages/EditPostPage';
import PostPage from './Pages/PostPage';

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
        },{
          path: "/myposts",
          element: <MyPostsPage />
        },{
          path: "/createpost",
          element: <CreatePostPage />
        },{
          path: "/mypost/:id",
          element: <MyPostPage />
        },
        {
          path: "/editpost/:id",
          element: <EditPostPage />
        },{
          path: "/post/:id",
          element: <PostPage />
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
