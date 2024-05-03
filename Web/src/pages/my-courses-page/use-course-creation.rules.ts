import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

interface Rules {
  title: Rule[];
  description: Rule[];
}

const RULES = {
  title: { min: 2, max: 64 },
  description: { max: 512 },
};

export default function useCourseCreationRules(): Rules {
  const { t } = useTranslation();

  return {
    title: [
      {
        required: true,
        message: t('my_courses_page.rules.title_required'),
      },
      {
        min: RULES.title.min,
        max: RULES.title.max,
        message: t('my_courses_page.rules.title_len', RULES.title),
      },
    ],
    description: [
      {
        max: RULES.description.max,
        message: t('my_courses_page.rules.description_len', RULES.description),
      },
    ],
  };
}
