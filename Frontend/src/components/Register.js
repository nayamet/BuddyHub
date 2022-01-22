import React, { useState, useEffect } from 'react';
import GoogleLogin from 'react-google-login';
import axios from 'axios';
import { useAlert } from 'react-alert';
import { Link, useNavigate } from 'react-router-dom';
import { apiUrl } from '../Config';
import FacebookLogin from 'react-facebook-login';
import { Formik, Form } from 'formik';
import * as Yup from 'yup';
import { TextField } from './mini/TextField';


const Register = () => {

    const alert = useAlert();
    const navigate = useNavigate();

    var [errorMsg, setErrorMsg] = useState('');
    var [oAuthValidationDone, SetOAuthValidationDone] = useState(false);

    useEffect(() => { document.title = "BuddyHub - Registration" }, []);
    var [registrationInfo, SetRegistrationInfo] = useState({
        Username: "",
        Password: "",
        Name: "",
        Email: "",
        ProfileImage: "",
        OriginId: "",
        OriginName: "",
    });


    const DoOAuthRegistration = () => {
        SetRegistrationInfo({
            ...registrationInfo,
            Username: registrationInfo.Username,
            Password: registrationInfo.Password,
        })
       
        console.log(registrationInfo);
        axios.post(`${apiUrl}/OAuth/Register`, registrationInfo)
            .then(res => {
                console.log(res.data);
                alert.success("Registration Successful.");
                setTimeout(() => { navigate('/login') }, 2000);
            })
            .catch(err => {
                if (err === undefined) {
                    alert.show('Something went wrong');
                } else {
                    console.log(err.response.data.Message);
                    if (err.response.data.Message === "Already Registered!!") {
                        alert.info('You are already Registered!!', {
                            timeout: 2000, // custom timeout just for this one alert
                            type: 'success',
                            onOpen: () => {
                                console.log('hey')
                            }, // callback that will be executed after this alert open
                            onClose: () => {
                                console.log('closed')
                            } // callback that will be executed after this alert is removed
                        });
                        setTimeout(() => { navigate('/login') }, 2000);
                    } else {
                        alert.error('Opps!!!', {
                            timeout: 3000,
                        })
                    }
                }
            })
    }

    const responseGoogle = (response) => {
        console.log(response);
        SetRegistrationInfo({
            ...registrationInfo,
            Name: response.profileObj.name,
            Email: response.profileObj.email,
            ProfileImage: response.profileObj.imageUrl,
            OriginId: response.googleId,
            OriginName: 'Google',
        })
        // OAuthRegistrationRequest(user);
        SetOAuthValidationDone(true);
    }
    const responseFacebook = (response) => {
        console.log(response);
        // let user = {
        //     Username: response.profileObj.email,
        //     Name: response.profileObj.name,
        //     Email: response.profileObj.email,
        //     ProfileImage: response.profileObj.imageUrl,
        //     OriginId: response.googleId,
        //     OriginName: 'Google',
        //     Password: response.googleId,
        // }
        // SendLoginRequest(user);
    }

    const SendEmail = (user) => {
        axios.post(`${apiUrl}/SendEmail`, user)
    }
    const DoRegistration = async (registrationInfo) => {
        await axios.post(`${apiUrl}/Register`, registrationInfo)
            .then(res => {
                console.log(res.data);
                var user = {
                    Username: registrationInfo.Username,
                    Name: registrationInfo.Name,
                    UserEmail: registrationInfo.Email,
                }
                SendEmail(user);
                alert.success("Registration Successful.");
                setTimeout(() => { navigate('/login') }, 2000);
            })
            .catch(err => {
                // alert.error('Opps!!!');
                if (err.response !== undefined) {
                    console.log(err.response.data.Message);
                    setErrorMsg("Error: " + err.response.data.Message);
                }
                else {
                    alert.error('Opps!!!');
                }
            })

    }
    const HandleRegistrationForm = e => {
        SetRegistrationInfo({
            ...registrationInfo,
            [e.target.name]: e.target.value
        })
    }

    const validate = Yup.object({
        Name: Yup.string()
            .min(3, 'Must be at least 3 characters')
            .required('Required'),
        Username: Yup.string()
            .min(3, 'Must be at least 3 characters')
            .required('Username is required'),
        Email: Yup.string()
            .email('Invalid email address')
            .required('Email is required'),
        Password: Yup.string()
            .min(4, 'Password must be at least 4 charaters')
            .required('Password is required'),
        ConfirmPassword: Yup.string()
            .oneOf([Yup.ref('Password'), null], 'Password must match')
            .required('Confirm password is required'),
    })

    return (
        <div>
            <div className="container">
                <div className="row d-flex justify-content-center">
                    {!oAuthValidationDone && <>
                        <div className="col-5">
                            <div className="card shadow-lg rounded-3">
                                <div className="card-body">
                                    <h4 className="card-title text-center text-capitalize fw-bold">Registration</h4>
                                    <hr />
                                    <div className="d-flex flex-column">
                                        <Formik
                                            initialValues={{
                                                Name: '',
                                                Username: '',
                                                Email: '',
                                                Password: '',
                                                ConfirmPassword: ''
                                            }}
                                            validationSchema={validate}
                                            // validate={values => {
                                            //     if(values.Username){
                                            //         const errors = {};
                                            //         const CheckUser = async () => {
                                            //             console.log(`${apiUrl}/User/Username/${values.Username}`);
                                            //            await axios.get(`${apiUrl}/User/Username/${values.Username}`)
                                            //            .then(res => {
                                            //                console.log(res.data);
                                            //                errors.Username = "Username already exists";
                                            //            })
                                            //         }
                                            //         CheckUser();
                                            //         return errors;
                                            //     }
                                            // }}
                                            onSubmit={values => {
                                                DoRegistration(values);
                                            }}
                                        >
                                            {formik => (
                                                <div>
                                                    <Form className='needs-validation' noValidate>
                                                        <TextField label="Name" id="Name" name="Name" type="text" />
                                                        <TextField label="Username" id="Username" name="Username" type="text" />
                                                        <TextField label="Email" id="Email" name="Email" type="text" />
                                                        <TextField label="Password" id="Password" name="Password" type="password" />
                                                        <TextField label="Confirm Password" id="ConfirmPassword" name="ConfirmPassword" type="password" />
                                                        <div className="row justify-content-end my-0">
                                                            <div className="col-8">
                                                                {/* Show The Error Respone From Backend */}
                                                                {errorMsg !== '' ? <div className="alert alert-danger p-1 px-3">{errorMsg}</div> : ''}
                                                            </div>
                                                        </div>
                                                        <div className="d-flex justify-content-end my-0">
                                                            <button className="btn btn-dark me-3" type="reset">Reset</button>
                                                            <button className="btn btn-success" type="submit">Register</button>
                                                        </div>
                                                    </Form>
                                                </div>
                                            )}
                                        </Formik>
                                    </div>
                                    <h6 className='text-center mt-4 text-muted'>-- or, register with --</h6>
                                    {/* Implement Login With Google OAuth */}

                                    <div className="row d-flex justify-content-center mt-4">
                                        <div className="col-4 text-center align-self-center">
                                            <GoogleLogin
                                                clientId="197200157088-jok25uj7eb4dm1jhdie8f5cth1kntimu.apps.googleusercontent.com"
                                                buttonText="Login"
                                                onSuccess={responseGoogle}
                                                onFailure={responseGoogle}
                                                cookiePolicy={'single_host_origin'}
                                            />
                                        </div>
                                        <div className="col-4 text-center align-self-center">
                                            <FacebookLogin
                                                appId="408009050991275"
                                                autoLoad={false}
                                                fields="name,email,picture"
                                                callback={responseFacebook}
                                                textButton="Login"
                                                cssClass='btn btn-primary'
                                                icon="fa-facebook"
                                            />
                                        </div>
                                    </div>
                                    <h6 className='text-center mt-4 text-muted'>Already Registered? <Link to="/login">Login</Link></h6>
                                </div>
                            </div>
                        </div>
                    </>}
                    {oAuthValidationDone && <>
                        <div className="col-5">
                            <div className="card shadow-lg rounded-3">
                                <div className="card-body">
                                    <h4 className="card-title text-center text-capitalize fw-bold">Set Username and Password</h4>
                                    <hr />
                                    <div className="">
                                    <div className="row mb-3">
                                        <label htmlFor="inputUsername" className="col-sm-3 col-form-label">Username :</label>
                                        <div className="col-sm-7">
                                            <input type="text" className="form-control" onChange={HandleRegistrationForm} name="Username" />
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <label htmlFor="inputPassword" className="col-sm-3 col-form-label">Password :</label>
                                        <div className="col-sm-7">
                                            <input type="password" className="form-control" onChange={HandleRegistrationForm} name="Password" />
                                        </div>
                                    </div>
                                    <div className="d-flex justify-content-center mt-4">
                                        <button type="submit" onClick={DoOAuthRegistration} className="btn btn-primary d-inline-block">Register</button>
                                    </div>
                                </div>
                                </div>
                            </div>

                        </div>



                    </>}
                </div>
            </div>
        </div>
    );
};

export default Register;