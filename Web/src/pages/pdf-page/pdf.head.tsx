import { Helmet } from 'react-helmet-async';

interface Props {
  fileName: string;
}

export default function PdfHead({ fileName }: Props) {
  return (
    <Helmet>
      <title>{fileName}</title>
    </Helmet>
  );
}
