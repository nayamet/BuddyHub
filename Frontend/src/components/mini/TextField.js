import React from 'react';
import { ErrorMessage, useField } from 'formik';

export const TextField = ({ label, ...props }) => {
    const [field, meta] = useField(props);
    return (
        <div className="row mb-3">
            <div className="col-4">

                <label htmlFor={field.name}>{label}</label>
            </div>
            <div className="col-8 position-relative">
                <input
                    className={`form-control shadow-none ${meta.touched && meta.error && 'is-invalid'} ${meta.touched && !meta.error && 'is-valid'}`}
                    {...field} {...props} required
                    autoComplete="off"
                />
                <ErrorMessage name={field.name} component="div" className="invalid-feedback m-0" />
            </div>
            {/* <ErrorMessage component="div" name={field.name} className="error" /> */}
        </div>
    )
}
