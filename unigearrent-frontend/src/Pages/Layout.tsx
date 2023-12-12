import { Outlet } from "react-router-dom";
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row'
import Navbar from "../Components/Navbar";

const Layout: React.FC = () => {
    return(<Container fluid className="min-vh-100" style={{backgroundColor:"#faf0e6"}}>
        <Row className="h-10">
            <Navbar />
        </Row>
        <Row>
            <Outlet />
        </Row>
    </Container>)
}
export default Layout;