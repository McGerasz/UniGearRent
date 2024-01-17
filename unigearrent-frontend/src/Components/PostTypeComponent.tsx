import { Col, Container, Row, Card, CardTitle, CardBody, CardText } from "react-bootstrap"
import React, { useState } from "react";
import { PostType } from "../Models/PostType";

const PostTypeComponent: React.FC<{postTypeSetter: React.Dispatch<React.SetStateAction<PostType | undefined>>}> = (props) => {
    const [carOptionIsHover, setCarOptionIsHover] = useState(false)
    const [trailerOptionIsHover, setTrailerOptionIsHover] = useState(false)
    const OnClickHandler: (input: string) => void = (input) => {
        console.log(input)
        if(input === "Car") props.postTypeSetter(PostType.Car);
        if(input === "Trailer") props.postTypeSetter(PostType.Trailer);
    }
    return (<Container className="mt-5 w-75">
        <Row className="text-center">
            <h1>What type of post would you like to create?</h1>
        </Row>
        <Row className="mt-5">
            <Col className="text-center" onClick={() => OnClickHandler("Car")} style={{cursor: "pointer"}}>
                <Card className="h-100" style={{backgroundColor: carOptionIsHover ? "#AC9362" : "white", color: carOptionIsHover ? "white" : "black"}} 
                onMouseEnter={() => setCarOptionIsHover(true)} onMouseLeave={() => setCarOptionIsHover(false)}>
                    <CardBody>
                        <CardTitle><h1>Car</h1></CardTitle>
                        <hr />
                        <CardText><h4>Create a "Car" post if you would like to list one of your vehicles on the site.</h4></CardText>
                    </CardBody>
                </Card>
            </Col>
            <Col className="text-center" onClick={() => OnClickHandler("Trailer")} style={{cursor: "pointer"}}>
            <Card className="h-100" style={{backgroundColor: trailerOptionIsHover ? "#AC9362" : "white", color: trailerOptionIsHover ? "white" : "black"}} 
                onMouseEnter={() => setTrailerOptionIsHover(true)} onMouseLeave={() => setTrailerOptionIsHover(false)}>
                    <CardBody>
                        <CardTitle><h1>Trailer</h1></CardTitle>
                        <hr />
                        <CardText><h4>Create a "Trailer" post if you would like to list one of your trailers on the site.</h4></CardText>
                    </CardBody>
                </Card>
            </Col>
        </Row>
    </Container>)
}
export default PostTypeComponent