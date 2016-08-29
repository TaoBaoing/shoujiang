using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicData.Domain
{

    //https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.1aJwL7&treeId=59&articleId=103666&docType=1
    public class AlipayAsyncNotifyLog
    {
        public long Id { get; set; }

        public DateTime CreateTime { get; set; }

        //获取所有的返回信息
        public string FormAll { get; set; }


        //错误信息
        public string ErrorMsg { get; set; }


        //订单类型 判断是购买E金还是E尊金还是会员升级
        public int? order_type { get; set; }

        /// <summary>
        /// 通知时间  2013-08-22 14:45:24
        /// </summary>
        public DateTime? notify_time { get; set; }

        /// <summary>
        /// 通知类型  trade_status_sync
        /// </summary>
        public string notify_type { get; set; }

        /// <summary>
        /// 通知校验ID  64ce1b6ab92d00ede0ee56ade98fdf2f4c
        /// </summary>
        public string notify_id { get; set; }

        /// <summary>
        /// 固定取值为RSA
        /// </summary>
        public string sign_type { get; set; }

        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }


        /// <summary>
        /// 商户网站唯一订单号  原样返回
        /// </summary>
        public string out_trade_no { get; set; }

        /// <summary>
        /// 该交易在支付宝系统中的交易流水号。最短16位，最长64位
        /// </summary>
        public string trade_no { get; set; }

        /// <summary>
        /// 商品名称  原样返回
        /// </summary>
        public string subject { get; set; }

        /// <summary>
        /// 支付类型 默认值为：1（商品购买）
        /// </summary>
        public string payment_type { get; set; }

        /// <summary>
        /// 交易状态，取值范围请参见 https://doc.open.alipay.com/doc2/detail?treeId=59&articleId=103672&docType=1
        /// </summary>
        public string trade_status { get; set; }

        /// <summary>
        /// 卖家支付宝用户号 卖家支付宝账号对应的支付宝唯一用户号。以2088开头的纯16位数字 2088501624816263
        /// </summary>
        public string seller_id { get; set; }

        /// <summary>
        /// 卖家支付宝账号，可以是email和手机号码
        /// </summary>
        public string seller_email { get; set; }

        /// <summary>
        /// 买家支付宝用户号
        /// </summary>
        public string buyer_id { get; set; }
        /// <summary>
        /// 买家支付宝账号
        /// </summary>
        public string buyer_email { get; set; }

        /// <summary>
        /// 交易金额
        /// </summary>
        public decimal? total_fee { get; set; }
        
        /// <summary>
        /// 购买数量
        /// </summary>
        public decimal? quantity { get; set; }

        /// <summary>
        /// 商品单价
        /// </summary>
        public decimal? price { get; set; }


        /// <summary>
        /// 商品描述 原样返回
        /// </summary>
        public string body { get; set; }


        /// <summary>
        /// 交易创建时间 
        /// </summary>
        public DateTime? gmt_create { get; set; }


        /// <summary>
        /// 交易付款时间
        /// </summary>
        public DateTime? gmt_payment { get; set; }

        /// <summary>
        /// 是否调整总价 N
        /// </summary>
        public string is_total_fee_adjust { get; set; }

        /// <summary>
        /// 是否使用红包买家
        /// </summary>
        public string use_coupon { get; set; }

        /// <summary>
        /// 折扣 0.00
        /// </summary>
        public string discount { get; set; }

        /// <summary>
        /// 退款状态 https://doc.open.alipay.com/doc2/detail?treeId=59&articleId=103673&docType=1
        /// </summary>
        public string refund_status { get; set; }

        /// <summary>
        /// 退款时间
        /// </summary>
        public DateTime? gmt_refund { get; set; }

    }
}
