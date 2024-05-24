import {
  isArrayOf,
  isArrayOfStrings,
  isObject,
  isString,
} from './type-guards.util';

type MessageError = string | undefined;
type DescriptionError = string | string[] | undefined;
type ValidationError = ValidationErrors | undefined;
type StatusError = number | string | undefined;

interface ErrorObject {
  message?: MessageError;
  description?: DescriptionError;
  validation?: ValidationError;
  status?: StatusError;
}

interface PossibleError {
  data?: string | PossibleErrorData;
  message?: string;
  status?: number | string;
}

interface PossibleErrorData {
  statusCode?: number;
  code?: string;
  message?: string;
  details?: string | string[] | DetailValidationError[];
}

interface DetailValidationError {
  code: string;
  message: string;
}

export interface ValidationErrors {
  [propertyName: string]: string[];
}

export function parseErrorObject(error: unknown): ErrorObject {
  if (!error) {
    return {};
  }

  if (isString(error)) {
    return { message: error };
  }

  const possibleError = error as PossibleError;

  return {
    message: getMessageError(possibleError),
    description: getDescriptionError(possibleError),
    validation: getValidationError(possibleError),
    status: getStatusError(possibleError),
  };
}

function getMessageError(error: PossibleError): MessageError {
  if (isString(error.data)) {
    return error.data;
  } else if (error.data?.message) {
    return error.data.message;
  } else if (isString(error.data?.details)) {
    return error.data.details;
  } else if (error.message) {
    return error.message;
  }

  return undefined;
}

function getDescriptionError(error: PossibleError): DescriptionError {
  if (isString(error.data) && error.message) {
    return error.message;
  } else if (
    !isString(error.data) &&
    error.data?.message &&
    isArrayOfStrings(error.data.details)
  ) {
    return error.data.details;
  }

  return undefined;
}

function getValidationError(error: PossibleError): ValidationError {
  if (
    !isString(error.data) &&
    isArrayOf<DetailValidationError>(
      error.data?.details,
      isDetailValidationError,
    )
  ) {
    const obj: ValidationErrors = {};

    for (const detail of error.data.details) {
      if (!obj[detail.code]) {
        obj[detail.code] = [];
      }

      obj[detail.code].push(detail.message);
    }

    return obj;
  }

  return undefined;
}

function getStatusError(error: PossibleError): StatusError {
  if (error.status) {
    return error.status;
  } else if (error.data && !isString(error.data)) {
    if (error.data.statusCode) {
      return error.data.statusCode;
    } else if (error.data.code) {
      return error.data.code;
    }
  }

  return undefined;
}

function isDetailValidationError(
  value: unknown,
): value is DetailValidationError {
  if (!isObject(value)) {
    return false;
  }

  return (
    'code' in value &&
    isString(value.code) &&
    'message' in value &&
    isString(value.message)
  );
}
