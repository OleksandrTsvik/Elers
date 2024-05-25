import { Rule } from 'antd/es/form';
import { useTranslation } from 'react-i18next';

import { COURSE_MATERIAL_RULES } from '../../common/rules';
import useValidationRules from '../../hooks/use-validation-rules';

interface Rules {
  title: Rule[];
  file: Rule[];
}

export default function useMaterialFileRules(): Rules {
  const { t } = useTranslation();
  const { trimWhitespace, fileSizeLimit } = useValidationRules();

  return {
    title: [
      {
        required: true,
      },
      {
        min: COURSE_MATERIAL_RULES.file.title.min,
        max: COURSE_MATERIAL_RULES.file.title.max,
      },
      trimWhitespace,
    ],
    file: [
      {
        required: true,
        message: t('course_material.rules.file_required'),
      },
      fileSizeLimit(),
    ],
  };
}
