import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { Container, Row, Col } from 'react-bootstrap';
import { Link } from 'react-router-dom';
const LoginComponent: React.FC = () =>{
    return(
    <Container className='w-50 mt-5 align-items-center'>
        <Form>
            <Form.Group className="mb-3" controlId="formBasicEmail">
            <Form.Label>Email address</Form.Label>
            <Form.Control type="email" placeholder="Enter email" />
            </Form.Group>
    
            <Form.Group className="mb-5 justify-content-md-center" controlId="formBasicPassword">
            <Form.Label>Password</Form.Label>
            <Form.Control type="password" placeholder="Password" />
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