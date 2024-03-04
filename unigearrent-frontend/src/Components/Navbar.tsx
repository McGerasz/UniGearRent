import React from 'react';
import "bootstrap/js/src/collapse.js";
import BootstrapNavbar from 'react-bootstrap/Navbar';
import { Container, Nav, NavItem, NavbarBrand, NavbarCollapse, NavbarToggle } from 'react-bootstrap';
import {Link, useNavigate} from 'react-router-dom';
import { useUserProfile } from '../Utils/UserProfileContextProvider';
import Cookies from 'universal-cookie';
import { RegistrationType } from '../Models/RegistrationType';

const Navbar: React.FC = () => {
    let cookies = new Cookies();
    let profile = useUserProfile().userProfile;
    const navigate = useNavigate();
    const profileSetter = useUserProfile().setUserProfile;
    const LogoutHandler: () => void = () => {
        profileSetter(null);
        cookies.remove("profile");
        navigate("/");
    }
    return(
    <BootstrapNavbar collapseOnSelect style={{backgroundColor: "#d9b99b"}} data-bs-theme="light" expand="xl" >
        <Container>
            <Nav className='me-auto'>
                <NavbarBrand>
                <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/">
                        UniGearRent
                </Link>
                </NavbarBrand>
            </Nav>
            <NavbarToggle aria-controls="basic-navbar-nav" className="ml-auto"
                    />
                <NavbarCollapse id="basic-navbar-nav" className="justify-content-center">
            <Nav  className="me-auto">
            <NavItem>
            <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/search">
                Search
            </Link>
            </NavItem>
            </Nav>
            <Nav className="justify-content-evenly w-25">
                {profile ? (<>
                    <NavItem>
                    <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/myaccount">
                        My Account
                    </Link></NavItem>
                        {profile.Type === RegistrationType.Lessor.valueOf() ? (<NavItem>
                            <Link to="/myposts" className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" style={{width:"100%", whiteSpace:"nowrap", overflow:"hidden", textOverflow:"ellipsis"}}>
                            My Posts
                        </Link></NavItem>) : 
                        (<NavItem><Link to="/myfavourites" className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" style={{width:"100%", whiteSpace:"nowrap", overflow:"hidden", textOverflow:"ellipsis"}}>
                            My Favourites
                        </Link></NavItem>)}
                        <NavItem  className="link-dark link-offset-2" style={{cursor:"pointer"}} onClick={LogoutHandler}>Logout</NavItem>
                        </>) : 
                
                (<>
                    <NavItem>
                        <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/register" style={{width:"100%", whiteSpace:"nowrap", overflow:"hidden", textOverflow:"ellipsis"}}>
                                    Register
                        </Link>
                    </NavItem>
                    <NavItem>
                        <Link className="link-dark link-offset-2 link-underline-opacity-0 link-underline-opacity-0-hover" to="/login" style={{width:"100%", whiteSpace:"nowrap", overflow:"hidden", textOverflow:"ellipsis"}}>
                                    Log in
                        </Link>
                    </NavItem>
                    </>)}
            </Nav>
            </NavbarCollapse>
        </Container>
    </BootstrapNavbar>
    )
}
export default Navbar;