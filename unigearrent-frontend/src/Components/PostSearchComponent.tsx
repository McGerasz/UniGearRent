import { Button, Col, Form, Row } from "react-bootstrap";

const PostSearchComponent: React.FC = () => {
    return(
    <Form>
        <Row>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicLocation">
                    <Form.Control type="text" placeholder="Enter location" name='loc'/>
                </Form.Group>
            </Col>
            <Col>
                <Form.Group className="mb-3" controlId="formBasicPoster">
                    <Form.Control type="text" placeholder="Enter poster name" name='poster'/>
                </Form.Group>
            </Col>
        </Row>
        <Row className="mx-auto w-25">
            <Button className="btn-dark" type="submit">Search</Button>
        </Row>
    </Form>)
}
export default PostSearchComponent;