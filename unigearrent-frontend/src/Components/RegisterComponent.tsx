import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { Container, Row } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import RegisterTypeComponent from './RegisterTypeComponent'
import { useState } from 'react';
import { RegistrationType } from '../Models/RegistrationType';
import UserRegistration from './UserRegistration'
import LessorRegistration from './LessorRegistration';
const RegisterComponent: React.FC = () =>{
    const [registerType, setRegisterType] = useState<RegistrationType>()
    const [typeSelected, setTypeSelected] = useState(false)
    return(
        <>{typeSelected ? (registerType === RegistrationType.User.valueOf() ? (<UserRegistration />) : (<LessorRegistration />)) : <RegisterTypeComponent registerTypeSetter={setRegisterType} typeSelectedSetter={setTypeSelected} />}</>
      )
}
export default RegisterComponent