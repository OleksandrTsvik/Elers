import { FormInstance } from 'antd';
import { FieldData } from 'rc-field-form/es/interface';
import { useEffect } from 'react';

import ErrorDetails, { ErrorDetailsProps } from './error.details';
import useParseError from '../../hooks/use-parse-error';
import { ValidationErrors, getUniqueArrayItems } from '../../utils/helpers';

interface Props extends ErrorDetailsProps {
  error: unknown;
  form: FormInstance;
}

export default function ErrorForm({ error, ...props }: Props) {
  if (!error) {
    return null;
  }

  return <ErrorFormContent error={error} {...props} />;
}

function ErrorFormContent({ error, form, ...props }: Props) {
  const { message, description, validation } = useParseError(error);

  useEffect(() => {
    if (validation) {
      updateFormErrors(form, validation);
    }
  }, [description, form, validation]);

  return (
    <ErrorDetails
      className="mb-field"
      message={message}
      description={getDescriptionFormError(form, validation, description)}
      {...props}
    />
  );
}

function updateFormErrors(form: FormInstance, validation: ValidationErrors) {
  const fieldNames = Object.keys(form.getFieldsValue(true) as object);
  const validationKeys = Object.keys(validation);
  const fields: FieldData[] = [];

  for (const name of fieldNames) {
    const validationKey = validationKeys.find(
      (item) => item.toLowerCase() === name.toLowerCase(),
    );

    if (validationKey) {
      fields.push({
        name,
        errors: getUniqueArrayItems(validation[validationKey]),
      });
    } else {
      fields.push({ name, errors: [] });
    }
  }

  form.setFields(fields);
}

function getDescriptionFormError(
  form: FormInstance,
  validation: ValidationErrors | undefined,
  description: string[],
): string[] {
  if (!validation) {
    return description;
  }

  const newDescription = [...description];

  const fieldNames = Object.keys(form.getFieldsValue(true) as object).map(
    (item) => item.toLowerCase(),
  );

  for (const [key, value] of Object.entries(validation)) {
    if (!fieldNames.includes(key.toLowerCase())) {
      newDescription.push(...value);
    }
  }

  return newDescription;
}
