import React, { useState, useEffect } from 'react';
import BootstrapNavbar from 'react-bootstrap/Navbar';
import { Col, Container, Nav, NavbarBrand, Row } from 'react-bootstrap';
import {Link, NavLink} from 'react-router-dom';
import { useUserProfile } from '../Utils/UserProfileContextProvider';
import Cookies from 'universal-cookie';

const Navbar: React.FC = () => {
    let cookies = new Cookies();
    let profile = useUserProfile().userProfile;
    const profileSetter = useUserProfile().setUserProfile;
    const LogoutHandler: () => void = () => {
        profileSetter(null);
        cookies.remove("profile")
    }
    return(
    <BootstrapNavbar style={{backgroundColor: "	#d9b99b"}} data-bs-theme="light">
        <Container>
            <Nav className='me-auto'>
                <NavbarBrand>
                <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/">
                        UniGearRent
                </Link>
                </NavbarBrand>
            </Nav>
            <Nav>
                {profile ? (
                <Row>
                    <Col>
                    <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/">
                        Profile
                    </Link>
                    </Col>
                    <Col className="link-dark link-offset-2" style={{cursor:"pointer"}} onClick={LogoutHandler}>
                        Logout
                    </Col>
                </Row>) : 
                (<Row>
                    <Col>
                        <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/register">
                                    Register
                        </Link>
                    </Col>
                    <Col>
                        <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/login">
                                    Log in
                        </Link>
                    </Col>
                </Row>)}
            </Nav>
        </Container>
    </BootstrapNavbar>
    )
}
export default Navbar;