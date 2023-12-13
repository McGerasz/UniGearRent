import { Col, Container, Row, Card, CardTitle, CardBody, CardText } from "react-bootstrap"
import { RegistrationType } from '../Models/RegistrationType';
import { useState } from "react";

const RegisterTypeComponent: React.FC<{registerTypeSetter: React.Dispatch<React.SetStateAction<RegistrationType | undefined>>,
typeSelectedSetter: React.Dispatch<React.SetStateAction<boolean>>}> = (props) => {
    const [userOptionIsHover, setUserOptionIsHover] = useState(false)
    const [lessorOptionIsHover, setLessorOptionIsHover] = useState(false)
    const OnClickHandler: (input: string) => void = (input) => {
        console.log(input)
        if(input === "User") props.registerTypeSetter(RegistrationType.User);
        if(input === "Lessor") props.registerTypeSetter(RegistrationType.Lessor);
        props.typeSelectedSetter(true)
    }
    const HoverHandler: (input: boolean) => void = (input) => {

    }
    return (<Container className="mt-5">
        <Row className="text-center">
            <h1>What type of account would you like to create?</h1>
        </Row>
        <Row className="mt-5">
            <Col className="text-center" onClick={() => OnClickHandler("User")} style={{cursor: "pointer"}}>
                <Card className="h-100" style={{backgroundColor: userOptionIsHover ? "#AC9362" : "white", color: userOptionIsHover ? "white" : "black"}} 
                onMouseEnter={() => setUserOptionIsHover(true)} onMouseLeave={() => setUserOptionIsHover(false)}>
                    <CardBody>
                        <CardTitle><h1>User</h1></CardTitle>
                        <hr />
                        <CardText><h4>Create a "User" account if you would like to rent the different cars/trailers available on the website</h4></CardText>
                    </CardBody>
                </Card>
            </Col>
            <Col className="text-center" onClick={() => OnClickHandler("Lessor")} style={{cursor: "pointer"}}>
            <Card className="h-100" style={{backgroundColor: lessorOptionIsHover ? "#AC9362" : "white", color: lessorOptionIsHover ? "white" : "black"}} 
                onMouseEnter={() => setLessorOptionIsHover(true)} onMouseLeave={() => setLessorOptionIsHover(false)}>
                    <CardBody>
                        <CardTitle><h1>Lessor</h1></CardTitle>
                        <hr />
                        <CardText><h4>Create a "Lessor" account of you would like other people to rent your cars/trailers</h4></CardText>
                    </CardBody>
                </Card>
            </Col>
        </Row>
    </Container>)
}
export default RegisterTypeComponent