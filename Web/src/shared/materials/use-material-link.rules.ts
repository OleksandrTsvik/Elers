import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_MATERIAL_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  link: Rule[];
}

export default function useMaterialLinkRules(): Rules {
  const { t } = useTranslation();
  const { trimWhitespace } = useValidationRules();

  return {
    title: [
      {
        required: true,
      },
      {
        min: COURSE_MATERIAL_RULES.link.title.min,
        max: COURSE_MATERIAL_RULES.link.title.max,
      },
      trimWhitespace,
    ],
    link: [
      {
        required: true,
        type: 'url',
        message: t('validation_rule.link'),
      },
      {
        max: COURSE_MATERIAL_RULES.link.link.max,
      },
      trimWhitespace,
    ],
  };
}
