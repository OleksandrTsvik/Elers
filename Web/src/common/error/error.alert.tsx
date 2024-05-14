import ErrorDetails, { ErrorDetailsProps } from './error.details';
import useParseError from '../../hooks/use-parse-error';

interface Props extends ErrorDetailsProps {
  error: unknown;
}

export default function ErrorAlert({ error, ...props }: Props) {
  if (!error) {
    return null;
  }

  return <ErrorAlertContent error={error} {...props} />;
}

function ErrorAlertContent({ error, ...props }: Props) {
  const { message, description, validation } = useParseError(error);

  if (validation) {
    for (const item of Object.values(validation)) {
      description.push(...item);
    }
  }

  return (
    <ErrorDetails message={message} description={description} {...props} />
  );
}
