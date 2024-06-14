import { Helmet } from 'react-helmet-async';

interface Props {
  title: string;
}

export default function MyProfileHead({ title }: Props) {
  return (
    <Helmet>
      <title>{title}</title>
    </Helmet>
  );
}
