import React, { useState, useEffect } from 'react';
import BootstrapNavbar from 'react-bootstrap/Navbar';
import { Col, Container, Nav, NavbarBrand, Row } from 'react-bootstrap';
import {Link, NavLink} from 'react-router-dom';

const Navbar: React.FC = () => {
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
                <Row>
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
                </Row>
            </Nav>
        </Container>
    </BootstrapNavbar>
    )
}
export default Navbar;