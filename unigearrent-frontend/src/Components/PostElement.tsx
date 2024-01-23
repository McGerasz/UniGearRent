import { JsxElement } from "typescript";
import { PostType } from "../Models/PostType";
import { Col, Container, Row } from "react-bootstrap";

const PostElement: React.FC<{PostData: any, PostDataType: PostType}> = (props) => {
    const priceVisualizer: () => JSX.Element= () => {
        const priceArr = [props.PostData["hourlyPrice"],props.PostData["dailyPrice"],props.PostData["weeklyPrice"], props.PostData["securityDeposit"]];
        if(!priceArr.some(element => element !== null)) return <></>
        return <Row className="mb-5 text-center">{priceArr[0] ? <Col>Hourly price: {priceArr[0]} HUF</Col> : <></>}{priceArr[1] ? <Col>Daily price: {priceArr[1]} HUF</Col> : <></>}{priceArr[2] ? <Col>Weekly price: {priceArr[2]} HUF</Col> : <></>}{priceArr[3] ? <Col>Security deposit: {priceArr[3]} HUF</Col> : <></>}</Row>
    }
    return(
    <Container className="mt-3" style={{backgroundColor:"#B39377"}}>
        <Row className="text-center mb-5"><h1>{props.PostData["name"]}</h1></Row>
        <Row className="mb-5">{props.PostData["description"]}</Row>
        {priceVisualizer()}
        {props.PostDataType === PostType.Car ? 
        <Row className="mb-5">
            <Col>Number of seats: {props.PostData["numberOfSeats"]}</Col>
            <Col className="d-flex justify-content-end">Can it be delivered to you: {props.PostData["canDeliverToYou"] ? "Yes" : "No"}</Col>
        </Row>
        : props.PostData["width"] === null && props.PostData["length"] === null ? <></> :
         <Row className="mb-5 text-center">
            {props.PostData["width"] ? <Col>Width: {props.PostData["width"]}</Col> : <></>}
            {props.PostData["length"] ? <Col>Length: {props.PostData["length"]}</Col> : <></>}
         </Row>}
    </Container>)
}
export default PostElement;