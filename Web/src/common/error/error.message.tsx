import useParseError from '../../hooks/use-parse-error';

interface Props {
  error: unknown;
}

export default function ErrorMessage({ error }: Props) {
  const { message } = useParseError(error);

  return message;
}
