import React, { useState, useEffect } from 'react';
import { useAlert } from 'react-alert';
import { useNavigate } from 'react-router-dom';

const ChangePass = () => {
    const alert = useAlert();
    const navigate = useNavigate();

    useEffect(() => { document.title = "BuddyHub - ChangePassword" }, [])
    var [passInfo, setPassInfo] = useState({
        Username: "",
        OldPass: "",
        NewPass: ""
    });

    return (
        <>
            <div>
                <h2>ChangePassword</h2>
                <form action method="post">
                    Old password: <input type="password" name="OldPass" defaultValue className="form-control" />
                    <br />
                    New password: <input type="password" name="NewPass" defaultValue className="form-control" />
                    <br />
                    Confirm password: <input type="password" name="ConfirmPassword" defaultValue className="form-control" />
                    <br />
                    <input type="submit" name="submit" defaultValue="Change password" className="btn btn-success" />
                </form>
            </div>
        </>
            )
}

export default ChangePass;