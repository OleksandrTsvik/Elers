import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

interface Props {
  title: string;
}

export default function CourseHead({ title }: Props) {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course_page.head_title', { title })}</title>
    </Helmet>
  );
}
