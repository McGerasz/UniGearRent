import { Button, Col, Form, Row } from "react-bootstrap";
import { PostCardData } from "../Models/PostCardData";
import BackendURL from "../Utils/BackendURL";

const PostSearchComponent: React.FC<{searchDataSetter: React.Dispatch<React.SetStateAction<Array<PostCardData>>>}> = (props) => {
    const SubmitHandler: (e: React.FormEvent) => void = async (e) => {
        e.preventDefault();
        const target = e.target as typeof e.target & {
            loc: {value: string};
            poster: {value: string};
        }
        console.log(target.loc.value);
        console.log(target.poster.value);
        if(!target.loc.value && !target.poster.value) return;
        if(target.loc.value != ""){
            let response = await fetch(BackendURL + "Post/byLocation/" + target.loc.value).then(resp => resp.json())
            let processedData = response.map((element: any) => {
                return new PostCardData(element.id, element.name, element.location, element.posterId, element.description)
            })
            props.searchDataSetter(processedData as Array<PostCardData>);
        }
        else{
            let response = await fetch(BackendURL + "Post/byUser/" + target.poster.value).then(resp => resp.json())
            let processedData = response.map((element: any) => {
                return new PostCardData(element.id, element.name, element.location, element.posterId, element.description)
            })
            props.searchDataSetter(processedData as Array<PostCardData>);
        }
    }
    return(
    <Form onSubmit={SubmitHandler} className="mb-5">
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