using Newtonsoft.Json.Linq;
using ScreenFIRE.Modules.Companion;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ScreenFIRE.Assets {
    internal static partial class Strings {
        //private static readonly byte[] CypherKey = { 0x53, 0x63, 0x72, 0x65, 0x65, 0x6e, 0x20, 0xf0,
        //                                             0x9f, 0x85, 0xb5, 0xf0, 0x9f, 0x85, 0xb8, 0xf0,
        //                                             0x9f, 0x86, 0x81, 0xf0, 0x9f, 0x85, 0xb4 };//@"NkNcd&7ZfYTmYfNj+RY+byY8#v3&Q-D?_*&jSK+$hkZWzK4MtAB+FB9ByLAV_65kmREqhzaxupk*k9TT?y!JZnU*6j!ETzRApeH-?2n7wq?DTsc2ZP2TkgPPe^^ch@Xy8ZAJBXDFjk!AU$Nd2fu*VgfB-YmnJD=HpG$MNaj^qc*NQ4aw+^=5HX^YSKv@RRQN@RRn8JDqtP8LC$4q_*Rz*Z-EG3a2SttXKDPKgBrt4Y+ssFX7sXg6WQ7gHK3Q-!LaX=ZUenpn#$$B-!!Bt#VQB&?y25nXaPQ+e^fyZX97u%aA@$J!55w!d2Wa&a$!#wgZz-fG7HJV?mHE5uYv$5BvaRRq$gv!MsW?AfczVZj6Cn3wnY%@aab!4Q$+4NpA*4kWXZz8cV*LwtUMT9LuHQx8X@S3CF7R33qYpT+f9n*9_YR@fR8wKpGmH&MncJ&DK6rVqx%E%M!$xWTw#$E=kcha%#54AH4!T$ct4cGdVHq8ftJb3STuZgC75B^A?u&sNz?q-6JHPD?Q5pJC$Ue_x%23P$@DqMftjK$ZxpctK$SpXcsc5aem2gfR3_cEK%ENwKtj2tm3wBqx&xLc#8cRKsJf8cZ5+9rjMMCAbAV!E5vgP-pTUu9L!Q65se*K-3E$y#QV^c5MEJef_hzXFc5+Lbq8tBeC^dvzbBr^t4K^-XA7Vd5RabKDkfs?s_n%TMy94&_JZDv=uW&?bhH4#J9VMwfh3z9$BCt$#Su9nTKH@vBP4C2r$J_gFx*2E*-x%rDcD3TcrVYTJ42Zgj_UyK5wRnrCrgUkhbG-=Zh$5CeDjbh9em=%sHp3WC%vaW#egG8sRtFVNUbJ98DrLEXR8%883SrFmP_u#Z3T4_2@HsWgXtsS$SV&AZHSmdZbd&_DPs$q3FB3_Rebr4_P2XFnxr$fb!t#aRwhJDCcxsDW&fWD-bK5DZ+L&$pGdzX8#!kE^PTDLA2P9hpj_EwsKb!%-v2U+-kQb3n_amJ5Y4jt@qek#cE2ChhK8YCzMLUvmsEctzTrmuH@D3$^6w+DxUsq#8pcGRg#2tkj4cH+#hFgpV_7hqEv6WHCjLK?Bm*LeFNCmhUXbx2r-5F^Me*8b8UJG$4WjXEJc?^jg6!QsrJ*fscTwFM%qS#NY2tc*ff?JLb&bFWZCFf*GfMga?2Fjp8X6ED6wqFdaMVb$dg7NaCy&#R-hp53w-s+B_LRPNQFR$Ak$K#Zmz_-=R!_QqtT=P7WUgxVjymaHw9bGN##bbRhBS*kVsBwve&rb$mcBV$*-kn7az_j+KkM3mDcufC!RL+p6E!PB47t%w*_fHf^FfdNT2US2GkD&e#^tG-Sv*LkcY9hsDa-ABm7g=DED6MjszyCuadSj8Vecqc3jt&spFhPgsG#JYg$_hVSAAc6wLW-37tay=wFh5y+Y!bAL5AdA87zhMGrX2Uu8c6MFgW_9x8$$ZP+PDHLCm8Lbf6z3t-kygq93Y$+@94vXm=G&uj$$$K7sv?f2aPW6hqJvKbXsCHwmWX_Vs4z37jnZqURsp3^PbAEmL2k2NFq2efaE-Uy#kH_?7%PfGTh+dCvP@^Z+e3*mPHw9MuB8JG53UY^?M-%ndN8_D_BXxsNQxWFRx#%MbdQFtT%*W+*3RzV53ukTL_XMJvTNR+f9B7Wpc@v@gxkjcNSu$U_7Eu&Lv6m-cXznZCPt5p?YAuqNdgcaHKva94=&6M34W@b-tm+9#nQQgG_M@w$WtZrNWUWhXcax*dSdth^39v+bxEHnM*MWWcbvN&bLerYLb8kfX9bsm++J%DuYPnB=*bpCxb!8sPhxXS=?Bb4axB33Ke!sa#!^eTmtwsv@sTVL$92JjbwRj4=HG-NvLGgTVzU8cdAdMuG39RUWLC#XVUrGNZmh6eMHpwguuAFxS^HPSLZ!=-C-s2?eEJ3*9JjPS7Mqp+Qg&@RBQ!mMGN?XgSb+F4s?nVf%URq+xEhjkSz=#^Vjyh265+#fDXB-HZA&XFvSPmVcQg^XFE_G^GmyMqRjt!SX-nm-2Zq5SAm?ywB8nJW+&6eNYXDyYFVxmK2Nd^=!GE3Efm=&qhyQ@$Q*L8QMNxaJPTrff7%3Zet%@gvMA#Zn-@PPwaXJwa98RPK=pC7&draCTJAZ!hs7y*%&@p@u&D=s6$R?rmtr%szYZ8#wr7P*QrhP9xTqMTH$^Fg$8tH^ZWMAdPyUu2dzrrxNUNyscmrHdrdgsRtMu!afHX3%HfT7Pytm@*+VU_Khwnqfu*@URnwga9JLf&8cgSzJP4p%Y?8Z7AV6TwEbDH4dk2%8AP9^qFwKrwW5ZBXgXzFfm5w@9@c$s$w3VNvxZU2wBb7rVbQBJ9Xs?v*_nF+55%sX-nvzv&+eJ*5mZAM5NVAKWJhddpkzSSk22%U!CM4+23d5X7Pj2%rWYE#HQ-PRAt9?ddG!MQudF8Ch!$p*aNuL2kChtbSQU+ky^bMpeug!6b$nc&rb##-F&-9e-ZFk_pQMtVmv%$#vt$#vmk#hy-u#gt!P$QUD=MNW4X*5j8!&yMW#t!6nG+ZJ$rbYd3ZBP7T=NHkzKF2K3-jxYP29XyNZud65+M_QDp!fNCqXM=YuKU3J5rsSd=vV8b*nA6DrN8SS*RL*Mube6Sp4zEG_sNQC$CZuezZ8*Rgc&LuQdDkbc&zfB@x4vmPyTN9cxWQ$U$9N-*CXrH&xCgn8MyY*b$XPt2!7crjT?6$erS9-3g?gd9&2WWPBG#3xX_pX?PNnKHEmPDQdU4u%GV%beaq-dFN%!j7_3_e#C^@+_uTrcHe8EtXH7BG_!uDnU65M-YnvWgtjQs%ZYPvemqDpA5?uZEr=C4!%f?3!4k*unF2RE%axp4QgF6%fsR+UjgB=9eKPYSZne32y8&hxB6g$4W3CVGHEcBSuD+2T3Lhj4LsVCNakmXep-T--u3NayW?dYNx?Z_-9mWT#P8+5nft9hk$aBghvn=Xxh?LbhU=tb*NRmUHAxDMD+t#vB+ha5Je-qJC#yV#X&uCGXDVhkD_Yz8u_sKu!LqCD^#fb-7^au^DxNqJW8EdX2fDd3PeX$$W7hJs4G5tQ#Z9Zy595mzKU_gq5awDX4sEE?dXhTF&A5Dr*&u+zx$&vk!kup%sZ$3rVCRYn8D@w^SWj-n2RAdW#Ar+fwF?!=KwHV&xygD=Uzb!GJREACdrna*GxTDT=nB*qN+fJ7Fv@h*VE9AhC6QFCNwAw@?K3#xYV2?NUFyVad_qCt=G+hm?H_EVXT38%CBPW4JRH5+jM2$k&=jP^jH4VNf2*^%&L&Ft+%fRWYd9rMBB&TKuHx=N?=Wn-?eJ+bwzLYU3!fkzkw5UFGU*KHNYNu#AQECeCr*N6dgG73q99t7neUgnSYsyetG2C7ySK%=pa2n9Bk&p-Cak?J#Ly2!zbr??p3P^zF=RUKTWY-&Ang#B^&r+8!%Dz9cf6!x-p-hvAw8XJ%TD%fTA_Z+n%q+MVWkTqxAPhCa8qt$?d@_$htwhXeKj*rprA@wWr3J-H44kGH98ePhy9fPxehfG^Rk7JGdTyd*fz5azWUx_R#*^PZQAM6!yStWP_+7#Y9u7K7czsdUc_M&UpUR*Rqt*zwf5%T=#*pe3?wfxNGj3?amW*T+hCR^TfJv_3=@_Y!LMdWTL_ndc%gbzUm8%-#c2Xq=*_T&+9SkEgjch23Uus_UnM5YXPwWymncE?hULYSCHMLvzN_$s@qw?*43wxPAFTvz*E@R=ZAwuJpbnxJ9Q@gasfAL+Esg&==-Vu9vE*f5ua@us5s@KpfP-n=xNfS7*d-eHnsd?z$xrs@J*SE59*5fMJzNSyrnB42n*#sJ94MzFXd?Z%tX8a4+S6#7svn%Yv3rB24A2vG?VtXfvUa#QM=T?vZF&z+6bxmVT4mvF6RLP78tU9dxv?Yy9du448T2n&47XwgLZ#63x@wkqQQ*YGW!SWKj4Xdb%gQg@cy+ebwJm?#*Uu#c4?@F_?-b6kS9*?jA$xVQ9Vz6=KzuASgh43jek@58b!L8f2ubgK#7bV7$?@8Jgg#DM#$Dsrxqz*NpRfR7Gm3Bqm3*K@Em+3kkFhaL&#Ln8MSWnVV4PP2=NJyQ3TghTD-q^AHx#A46UJ^qJKQASrPJr4wQSZmn$Au4ufe^!-8ZV8VqY!ye&e+Yg4cjRwS#8t@C6=3E$CzBq!e^zyg@Lk5*$mQ!X$3SF@W3cvHWn^n_YxLS%6k6Sb%eE9A6k6z5RxbSVUGn666hMjrm7#3kYceX^P4XA9Msm%@7c4$2DTJSE*gg2KGaNj2BQgWWmBY2twCVR5b$@@ehqYMBnjn^z9VR-VFT5hwu6C?u^FbhyNU4=P+sDmh6$re%QUz9FH9T@!=2xgY+Tsff?E%za5+ymD@Vm*kdwkRS5QZvvGR2m^qeb^B@s4He=b-VU5nVLRmREs2j453C2shh_s?xun9u3nqF3EH_=d?AGBy@DhV-h_%nGKkNX5^aay#nX*MsJTw=er3n$eeE93PheJTz$yBs@t#_dZetQE_PTaghF=Gqv!fsZbfd5c^pXg^Xh6bZSps-QmEL@xvprABEEecs9DaYsqCM?d+cnz%JhTwaecBDQ6-*7vE*Jf=G^7VL@qvetNS?aGQ!3hqW+Wr^f9rbxkVdt?WaJQj$3m!7-j^t$f?MJS%WB4+bFh*eCd#M-?6B3C%jAKkbRJgLGr28Az9YP_KgtyfEUu^cvQYweuaBVD?VaRDACS%9AD+HSeCHs8$495bxKEASZG*$YRz+mC?tFJab$U@jD8-hQ-8&4T6YF7%T8p-qsAJdwmTB+3#d3kkw3CfA74EXY*U@swWdCdH2_aF5W=jKEuKQgqyr@YzC6@Bqbrh$#B=uq@RyWQVyzcn&kzPAKD8DU#vC&u=bHmsLgM!-fe76BMtuaZ2Ejav7fB56XKAx3wAJVrt_j9+TKUnt2GSWWkMXMx!k%D!W_cK89Z9AmbcbwjJ&B+?W_cwcTjDzzhSdD2YzznJn4xKj$qg9YQYz2Vb-t@J9Qj*gC_L2C%$Ag=s&TACNsRVn_JdqG33n9SmJD%8yTGe=LmJbN^V^XLC6k@Ft_AK#AZ=puT$LBjrL4R6R^P68nGq9=sU2aDa8vYGq@6rn9qzBjS5Y=#K4J8hS2D2c-tBj#9Ng-?Z_tBh%PK-RTBsQ&qGdCPz6H4+mG%BXWP@tgTag=Wat8ETQHmULD@f^MtdanDYF=R9?5D$GmNn_khyJR2Q3wjbgS_NXE*&dh7V9&kz6uQhL*j8!jZ%ceEPhgMk*zb%PfbfFa8&%VBtp%Z9#A^AmhM-Th&4VQ=Kn35UMYW2F^zX-YKE=TKq+_6%FPKVHtJZejNEBdnf5g?LsCq^gNfxsedpThQCyGc-p@uhf7+QW#5uy^-4WS2!DFZex-R-r@3CP*q$bsG$e9^K8&rTW4*wkYw89G@nP43jMSv#bXQpJX=G$TwqjGPf#NWw^aYtyv2_*Gmkj23qjA?5$Ez2@n7p&rTdED4sj&3hdFLyfNnuG9nN=$n-T^tJ^zjFgMQGTJbp9xEE+n@vsZ_D&WKp65heStYA$?aw3&A5ETeewY4_bV#CM9P#d#jqLwy^zMUwC2kd2HxyvQ&WTY+-T8$Gwu_s%38EtzRp8DwMD-sB-AqTrTf=^#x#HWH?_Uv^JXs+yZ_aDxrsT4EpZ6Rm75$z#HSypJ$8#5D+H44$vF-s5U*U&*x_+@RHQcfhpacfyPCT%E*g8=3_f4Z#BUWqfLVcJfuw&KqkppqVD@K%n%rREXvhhqZ?cm+7cVW6CT4_rw9dD_Ebej6g!HmtJEJTSF7v8x#SF&M9KbP!W7Zhd@a+H-Q7ZHSfuT*3ZzEN%P3U$RN4B=9@Zc^B#egXrLXvA?bF7Hm+?tPDPX*fnxm4HB8-n8jQSWStVpu66uFt4@c9AawAU+_44$T_eb^hn?Ne&P5aYAg9$c2F$Xbr_U3Fx*T-QQChkxSJz@A3rf_6-2K#dR%dTbS$s?DeKrtYdYME$q@Us&eHGar89Q2*-Jw$aZZeejpXf_nAsgB9PFXNtvpYp6hmzA-_a6bE*N7TC!b*kYcs$YbLpc&am9btDZNV!S*E5C_AKpjev@P*#r89CBk3!ky7ESe6Unps@BTrVNa!9YeTEmVzLndBQH&kT#EJ%NnxBZDrxzJfD_Dxyn5hy!BSHBa_MAtXYFS_3qwrLLkGVntSjmVwRzJ#X6sXD9Z^Yf8&Sx-JZgJ2Z5NXeYzQT$dhmp-jEYPWJuCbGBBqyp2bE!e9k@+hhpZpK653XBPYG%%q2nX_VhPs4Kh@+VZbd?JZrNnkHS5k-AAHWNyu$ctC+@wfAVSD&2c*D3TQJ=u^YEX!p9&rQ#&Z9bn2n5R!SJayD@V?%xUZ-VfQ#4dh6T9xwXMDqzyw7f%DH8qbSpUkn2FBnfJs*VT?R29rT%95rXTUNKp+JPw!$dFm89yrMSe_!N@!g=Ug=SJPj-JDMy#+#hXgkhJUWPtJgg%Ft-xQ%ETjcyxz&jkMbJS4g#p&5n^LX$LWawVXhx^J+kr*JdxNP?_LYyv!?N&nyC3zwPF8k7QwV5&JHs4Rk-%Q69NvjtYk^LwwtN+E9Zh3^G3Q5M3E+UNAjB&aTbSkkrRBMrUwXGU#bR84t+$f5-mZhuYV^8*YS%F2Y#!=guen2a$-DKE3WyAh%kUS@VDzg$^sRztRwJB&=yFQbSDg&z5NLAW&JJyBNE&Ec9Qp^Whshuy$mmq@^4U9?kv_AMc#Yfg_D*?tx*pw4e@#r+cMpp$5M4tEF?T74w#CFfcn*6Cr6A4t@3-nTpMuES4mJ&ggwv&wa8rg6@DyWQ-td5A8unwpaRm%$GbUZnRWVuY&%k@zYX%XfZv-fmT5#MEy*krBeMcAWsFHBfY$ScKVhM$QnuqhJN6hwMba3wzR_BCcMWpFRzxYGrt2Wj#xa=V5nckBh#bm8?hcYE2JcHhJxx$k7Hq5UV7RF!V%_BAY7Drgfw?tJNwSCszY%d#mW!puBC_dx_h#k$?D5ee8t=?r@uT4M_HuthJgzAd$jPqUfyXY_4NQ-yx+uf28a7EYJnKUCcm$q$-MJ9^WPy*f&MQp?!QsQy+hpbL8*JPu5Up+BFT3Y-6!bs$8cjtJX?ySd2WNfvvn%!F2#cyW!dwQZ?*mCW4g%c-V9!tA*ZgMhB38vQGM#4bDk@6j*JYZa@LRDCwynfgt2uHUrNMYGG!c2A6sX_WhaY5v$ASdqJ#xf_gW6qqNXyPDBJEDkE_U8vzky6?BVVfxYxzEUWzVQ@3$9aK@5AHU49_4QrPR$9FBvBCj3Qan=H$CvvmrdKa*&Jz*LKbK3t-QDj^Azbvtv35DsbSAHn3FDrFnEbn-qTc#KmP-t4KNsrjPyY68mpMH$p^?RCCt!NHde-wCCDKwVrSzq7m23J$Yvbr+#^YMxuJAt3gY5%Cm8H!Y?BcQsdmr%^gafzs!uv5W^GTD3F%*pcaX#Jye#XGeD@fN=r&?erwS^u2B7S&Ngz6W3Vg!Hhb_Zc#JJhPnU_=kdZMgc#Ckse@VgrMwt4Fj5QhGpD!e59%^T-G%efNT^HPL%=gzdqa+eL+$8L_9XqF$Fe59SPnY9C!?XE^Mv9=y*XcmDV-@K-pkQkrkZ+N!!_sQ-m%Rx99Pmx8PJCKZBpbByZ-nVh-?*@u!RPsCVwFydS^&_JkZJ53a6Q!yjSH3H-@9juU3UKs?#&zzPMxgMm_sD7kXjkm&h-9dG?7=Wsq-?NYMdykBPhe*d?@dh6ZVpzW6GX!w6yD9u=ysQyV#kdKS_yXy2fYT^8D^jRM7pcDLjv9-EAr3%UA27HqCK&SLkn5yseUh9P8K&wS^ZntcgjetBdKAkUg@&KeE&LmHff@wpbH@-a7-k@CcByYg&hB$Es2uz=Fe7T!GC!B4g=!VBQ-=RsCYZgpf-6@TeDxRA?Pw6U+RHD^as?wu#euBXFf=tfLFXLh9fN7qQ#v=6U7yx_2jGVSTry*2*UTJW_eC@G!6dF3zb$v8Q&mbMVup-8k&HDnUXnUvU?t%+acT!3X$e_NU*2&yrfJ_tK9HZcbZ$pT!uf&aSf@c-4vq2F&sjLRu@EaXctaLGNcWtE&6hxLxMmFTp*BE@h-cL?m$a*yyyYSZJ&C9UtYn3HLcQdD^RUSpZMAzdeT!?5B@VZTh+yezuQd$DVvhuKASQ-MG3jqVA@S8zT=qzVKdMu6$B683pqZsvbu45Q32se4KuDWjhP!xJk-raUaC9*!Vj$eywFVyQ3A?T^TEe35U5=*VwsZekyPPWHVnU*6s8a3hRjR46pe!YZZ@_MFYFs@D?Pu--Zr3AMKLv5HC4h*P+n&P!cbLK=QsujVNEYgQJZC^uf4K$9FxKDv4Yn*nMq%-6^a6$9Qt?k7UqcK&H&dzdfv=b^S3DsgMtw&feS+C!e%La9S#hq=aAn%pjus@y64F=DP5-k8&eQZ#DAqPfmjfHtVqpQ9=+b39e+vEm3$?mf3hvs%c%d4gqsejn+fRhUB^CyEUSse#rfZ!jdRr58F5UaS4*P%y&GC=aq4Ptt#Gk46C-!8geDWf$B2wXBBJG@exc!RDNCRBydNbK-cLBj9qsJP=#k2%Tq+Xn6Cpm+m*FYnxEKxNaE6J92!%j=V8ZHE8=T*kb3?eJM?7vAys3nW9f7&FH**BGXkqDjM^wyGc&WgJuzeChgHUr3f^^6Ph*FLsMm9#z$mWEdbtgh2rfdBbdNuJHbQS%mhGs%@5Nw*VGp3KmhwHfsE_L@yc*apcKJZ-8pg*Z!mD@jktnsjVPk%6wD_@U7t+3Qb+8qEUHSpMbuxtCGeLtxpb!!w5T2wTXSjzJzMeAetm8!%SSf3$gnFYKL2aU&U^qd=dd7DVG+5g5PVsTFtujQ!^A_*6zRhP2D#w9ejjKTEzvqwkvc&2A6VbcgFjeqCakksKCyE*sRWj*pYBWa%m6==-Tv2$_nf@hKC@MUMAavdy@ZhkC7HvQ@%vE&eb$ALJ*7!wBt6CjWBgT83qR+nReEKGAy_T%QUHh76^A$@y+ATLY=+#LdntYAXGQ@q^xRf?_7SnmMnF7TT3U-K#mp#B6_25S!gU4@3Y2G!?snCz^@5YzfvPs-5^?GyS-4buEhRM8NY-8$2DAPx7HZBdr@QHr8HRe$GvQYQeSaP3eGVjL^@Mh@VN^wG+Vz8c5=g2+#aGt?XR9=f$ANr7!=CmF!@NbagmLfhaN5qsNemUe#E2NRzNNJCRdLwuQeDnJk+Sdx-sj-4uuK=x$g8dgkcB2N8&WY8A=47LsEFa-#Z-28E2Tam9cj!cS_L$t7ZJzr-8?R#L3Ckc%7aj$V9tUAZ5jCF%-cVY&?=n_8J4mU^bjrcGxR%T$b5kx+x*mLRErqvff6=ACD%AAYKkLExztL%Qp+Dd8v3K2FVgj-3J8eMCpg4@@DW$tTt!MARR%K43gC?mfxa!4MJrz#HRyh$tWL+V-y#qV%bfPcG-^AE8NhL6vF&kgP^?n&uKBWU$PMtBs$uC7nL7@4BJscfFwvx9%pYxB5x*MH4!q!u+*Px57!9PXB&4Q4^KV%_6n^UN-m?Wm5N6#p%#sb3RNW5WYz5EA@qZ54+2#y@9@*YQNePSmC7DcMKbZ?qh-&r3KHLG&G!qjYfbHZBD#b8znkLb4&uLH5kZJ63W6!8mmkuhN#$8WC_wMr%dQDmZL5jLm&J!Nn2p4SqnEb?GE$*=xbBZaNHg_rPybNJmqxTEug6UQ!VUDuqE_bvR5EjWA5?S-de_5XL2dWamGVd-JCVpHscEYX#QXwtaRnDVRB9yj-L^=MF$a8xP$j^F33&qP6!rAT45vRR+Q8vvHQK-Bs6tUJRhJAC4wgp&q#aBJ^h%?ELex+6x9p&-YT&xJXTR=5aeKY$BKc-YV$C@J3bs-%Yf$Su48#4m-kNyZa6&t6LRa?cx@7=f4Y?9?twsZ9cATT!Ck+xCQ?T+q!L+SPat&?&THwE!X*pc723LD#Y$AvzrWEbPTyk#*C@!%_mpxB=-&WagPMVfgMNd9yUj&c%DMwPMJEFhb^X8qd#p$5gmvg=VXSSYS9Srs*ed5YBWNz62GzM2Y8ezrj?N4-wKA#XYk6*xa^U5tcF%hxKk%Z6TYeCmcvvMuM@fesXZvYfQ&*RtLWkfD_M_!sA&TDb&fstGrRqgW4E3Cj#B@hVtZATf*9JSSTjrhpZ825#34UzWC4SyvdkFphC&8KBa-q@WN6ERN%*W#hp^qgA!7tg4X2n-Z^5BzG&F%QEnCB$B#sPuZ95!StLT_Sa6TJK#wD4zW7AkW%ZY549DMLJ2#YCk*!whW_dJr6%%Y62&ZGsJWht8PNhe8J4eXG+gx_bP!+ruFHMCtVR7z6%&Tv";

        private static readonly string LocalePath = Path.Combine(Common.SF_Data, "Locale");
        private static string JsonPath(ILanguages language) => Path.Combine(LocalePath, $"{language}.SF");


        private static Dictionary<IStrings, string> StringsStore = new();

        /// <summary> Pull cached strings according to <paramref name="language"/> </summary>
        /// <param name="language">Target <see cref="ILanguages"/> </param>
        /// <returns> Strings from cache (json) as Dictionary&lt;<see cref="IStrings"/>, <see cref="string"/>&gt; </returns>
        private static Dictionary<IStrings, string> StringsStore_FromCache(ILanguages language) {
            try {
                //? Pull from cache
                string stringsFromCache = File.ReadAllText(JsonPath(language));

                ////? Decrypt
                //string decryptedString
                //    = StringCipher.DecryptStringFromBytes_Aes(stringsFromCache,
                //                                              Common.Settings.LastAes.Key,
                //                                              Common.Settings.LastAes.IV)
                //        .ToString();

                //? Read & Convert
                return JToken.ReadFrom(new JTokenReader(stringsFromCache))
                            .ToObject<Dictionary<IStrings, string>>();

            } catch { return null; }
        }

        /// <summary> Determine and get the strings store that contains more strings </summary>
        /// <param name="language">Target <see cref="ILanguages"/> </param>
        /// <returns> The store that has more strings </returns>
        private static Dictionary<IStrings, string> PreferredStore(ILanguages language) {
            //? Compare >> false if Storage contains more all+more strings that are in json
            Dictionary<IStrings, string> stringsDictionaryFromCache = StringsStore_FromCache(language);
            if (stringsDictionaryFromCache == null) return StringsStore;

            foreach (IStrings key in stringsDictionaryFromCache.Keys)
                if (!StringsStore.ContainsKey(key))
                    return stringsDictionaryFromCache;
            return StringsStore;
        }

        /// <summary> Rebuild the strings from cache <br/>
        /// While also choosing the better source of strings (Runtime VS Cache) </summary>
        /// <param name="language"> Target language </param>
        /// <returns> true if succeeded </returns>
        internal static bool RebuildStorage(ILanguages language) {
            //? No saved locales were found
            if (!Directory.Exists(LocalePath))
                return false; //! Report failure

            //? Check if the locale file exists
            //?     >> Fallback to English
            if (!File.Exists(JsonPath(language)))
                language = ILanguages.English;

            //? Recheck & Cancel if no English
            if (!File.Exists(JsonPath(language)))
                return false; //! Report failure

            //? Rebuild runtime dictionary
            StringsStore = PreferredStore(language);

            return true; //! Report success
        }

        /// <summary> Store the strings in cache for performance <br/>
        /// While also choosing the better source of strings (Runtime VS Cache) </summary>
        /// <param name="language"> Target language </param>
        /// <returns> true if succeeded </returns>
        internal static bool SaveStorage(ILanguages language = ILanguages.System) {
            //? Make sure the Locale dir exists
            if (!Directory.Exists(LocalePath))
                Directory.CreateDirectory(LocalePath);

            //? Get system language if generic (System)
            if (language == ILanguages.System) language = Languages.SystemLanguage();
            //? Save last used language
            Common.Settings.Language = language; Common.Settings.Save();

            //? Cancel if the cache is better than the runtime strings
            if (StringsStore != PreferredStore(language))
                return false;

            //? Build json object
            JObject stringsAsJson = new(from item in StringsStore
                                        orderby item.Key
                                        select new JProperty(item.Key.ToString(), item.Value));

            ////? Encrypt
            ////  >> Store this Aes for next startup
            //Common.Settings.LastAes = Aes.Create(); Common.Settings.Save();
            //string decryptedString
            //     = StringCipher.DecryptStringFromBytes_Aes(ObjectToByteArray(stringsAsJson),
            //                                               Common.Settings.LastAes.Key,
            //                                               Common.Settings.LastAes.IV);

            //? Write json into locale file
            File.WriteAllText(JsonPath(language), stringsAsJson.ToString());

            return true;
        }
    }
}
