import React from 'react'

function ForgotPass() {
    return (
        <>
        <div className="container">
                <div className="row d-flex justify-content-center">
                    <div className="col-5">
                        <div className="card shadow-lg rounded-3">
                            <div className="card-body">
                                <h4 className="card-title text-center text-capitalize fw-bold">Login</h4>
                                <hr />
                                {/* <div className="">
                                    <div className="row mb-3">
                                        <label htmlFor="inputUsername" className="col-sm-3 col-form-label">Username :</label>
                                        <div className="col-sm-7">
                                            <input type="text" className="form-control" onChange={HandleLoginForm} name="Username" />
                                        </div>
                                    </div>
                                    <div className="row mb-3">
                                        <label htmlFor="inputPassword" className="col-sm-3 col-form-label">Password :</label>
                                        <div className="col-sm-7">
                                            <input type="password" className="form-control" onChange={HandleLoginForm} name="Password" />
                                        </div>
                                    </div>
                                    <div className="d-flex justify-content-center mt-4">
                                        <button type="submit" onClick={DoLogin} className="btn btn-primary d-inline-block">Sign in</button>
                                    </div>
                                </div>
                                <h6 className='text-center mt-4 text-muted'>Not Registered Yet? <Link to="/register">Register</Link></h6> */}
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
        </>
    )
}

export default ForgotPass
