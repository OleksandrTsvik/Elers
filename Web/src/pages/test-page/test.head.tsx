import { Helmet } from 'react-helmet-async';

interface Props {
  title: string;
}

export default function TestHead({ title }: Props) {
  return (
    <Helmet>
      <title>{title}</title>
    </Helmet>
  );
}
