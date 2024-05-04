import { Helmet } from 'react-helmet-async';
import { useTranslation } from 'react-i18next';

interface Props {
  title: string;
}

export default function CourseEditHead({ title }: Props) {
  const { t } = useTranslation();

  return (
    <Helmet>
      <title>{t('course_edit_page.head_title', { title })}</title>
    </Helmet>
  );
}
