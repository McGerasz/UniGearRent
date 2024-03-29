import RegisterTypeComponent from './RegisterTypeComponent'
import { useState } from 'react';
import { RegistrationType } from '../Models/RegistrationType';
import UserRegistration from './UserRegistration'
import LessorRegistration from './LessorRegistration';
const RegisterComponent: React.FC = () =>{
    const [registerType, setRegisterType] = useState<RegistrationType>()
    const [typeSelected, setTypeSelected] = useState<boolean>(false)
    return(
        <>{typeSelected ? (registerType === RegistrationType.User.valueOf() ? (<UserRegistration typeSelectedSetter={setTypeSelected}/>) : (<LessorRegistration typeSelectedSetter={setTypeSelected}/>)) : <RegisterTypeComponent registerTypeSetter={setRegisterType} typeSelectedSetter={setTypeSelected} />}</>
      )
}
export default RegisterComponent