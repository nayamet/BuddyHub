import React from 'react'
import { NavLink } from 'react-router-dom'

function NavBar() {
    return (
        <div>
            <NavLink to="/login" className={c => c.isActive ? "text-danger text-decoration-none" : "text-decoration-none"}> Login </NavLink>
            <NavLink to="/register" className={c => c.isActive ? "text-danger text-decoration-none" : "text-decoration-none"}> Register </NavLink>
        </div>
    )
}

export default NavBar
