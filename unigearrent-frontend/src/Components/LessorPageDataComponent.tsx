import { useEffect, useState } from "react";
import { Card, CardBody, CardHeader, CardTitle, Col, Container, ListGroup, ListGroupItem, Row } from "react-bootstrap";
import BackendURL from "../Utils/BackendURL";
import { PostCardData } from "../Models/PostCardData";
import PostCardsComponent from "./PostCardsComponent";

const LessorPageDataComponent: React.FC<{lessorData: any}> = (props) => {
    console.log(props.lessorData["posts"])
    return <Container className="w-100 mb-3 mt-4">
        <Card style={{backgroundColor:"#B39377"}}>
            <CardHeader className="text-center">
                <CardTitle><h1>{props.lessorData["name"]}</h1></CardTitle>
            </CardHeader>
            <CardBody>
                <ListGroup>
            <Row style={{backgroundColor:"#FFFFFF"}}>
                <Col className="w-50 p-0">
                    <ListGroupItem><h4>Phone number: {props.lessorData["phoneNumber"] ? props.lessorData["phoneNumber"] : "log in as a user to reveal"}</h4></ListGroupItem>
                </Col>
                <Col className="p-0" style={{textAlign:"right"}}>
                    <ListGroupItem><h4>Email address: {props.lessorData["email"] ? props.lessorData["email"] : "log in as a user to reveal"}</h4></ListGroupItem>
                </Col>
            </Row>
            </ListGroup>
            </CardBody>
        </Card>
        <Container className="justify-content-md-center w-75 mt-5">
        <PostCardsComponent searchData={props.lessorData["posts"].map((element: any) => new PostCardData(element.id, element.name, element.location, element.posterId, element.description)) as Array<PostCardData>} myPost={false}/>
        </Container> 
    </Container>
}
export default LessorPageDataComponent;