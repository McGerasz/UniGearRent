import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { Container, Row } from 'react-bootstrap';
import { Link, NavigateFunction, useNavigate } from 'react-router-dom';
import { useUserProfile } from '../Utils/UserProfileContextProvider';
import React, { useState } from 'react';
import BackendURL from '../Utils/BackendURL';
import { jwtDecode } from 'jwt-decode';
import { Profile } from '../Models/Profile';
import { RegistrationType } from '../Models/RegistrationType';
import Cookies from 'universal-cookie';
const LoginComponent: React.FC = () =>{
    const cookies = new Cookies();
    const navigate: NavigateFunction = useNavigate();
    const url = BackendURL;
    const userProfile = useUserProfile();
    let correctLogin: boolean = true;
    const [successfulLogin, setSuccessfulLogin] = useState<boolean>(true);
    const LoginHandler: (e: React.FormEvent) => void = async (e) => {
        e.preventDefault();
        const target = e.target as typeof e.target & {
            email: {value: string};
            password: {value: string};
        }
        const data = {
            email: target.email.value,
            password: target.password.value
        };
        type LoginResponse = {
            id: string,
            email: string,
            userName: string,
            phoneNumber: string,
            token: string
        }
        let noError: boolean = true;
        let response;
        response = await fetch(url + "Auth/Login", {
            method: "POST",
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(data)
        }).then(res => {
            if(res.ok){
                noError = true;
                correctLogin = true;
                return res.json()
            }
            else{
                correctLogin = false;
            }
        }).catch(error => {
            console.log(error);
            noError = false;
            alert("There was an error during login");
        }) as LoginResponse;
        setSuccessfulLogin(correctLogin);
        if(!correctLogin || !noError) return;
        type jwtDecodeType = {
            exp: number,
            "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string,
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress": string, 
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string,
            "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string,
            iat: Date,
            jti: string, 
            sub: string 
        }
        let profile = new Profile(response.id, response.userName, response.phoneNumber, response.email, response.token, RegistrationType[(jwtDecode(response.token) as jwtDecodeType)['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] as keyof typeof RegistrationType]);
        userProfile.setUserProfile(profile);
        cookies.set('profile', profile, {expires: new Date(Date.now() + 30*60000)});
        navigate("/");
    }
    return(
    <Container className='w-50 mt-5 align-items-center'>
        <Form onSubmit={LoginHandler}>
            <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="email" placeholder="Enter email" name='email'/>
            </Form.Group>
    
            <Form.Group className="justify-content-md-center" controlId="formBasicPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control type="password" placeholder="Password" name='password'/>
            <Row className="mb-3" style={{color:"red", visibility:(successfulLogin ? "hidden": "visible"), justifyContent:"center"}}>
                The provided email and/or password was incorrect
            </Row>
            </Form.Group>
            <Row className='w-75 mx-auto'>
                <Button className='btn btn-dark' variant="primary" type="submit">
                Login
                </Button>
            </Row>
            <Row className='mt-3 text-center'>
            <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-50-hover" to="/register">
                        Click here to register
            </Link>
            </Row>
        </Form>
      </Container>
      )
}
export default LoginComponent